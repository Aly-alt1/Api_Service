using Api_Services_Rest.Dmain.Data;
using Api_Servixe.Mapper;
using Api_Servixe.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Api_Servixe.Service.Usuario
{
    public class UsuariosService
    {
        private Api_Services_Rest.Dmain.Data.productosDbEntities dbEntities = null;
        public UsuariosService()
        {
            dbEntities = new Api_Services_Rest.Dmain.Data.productosDbEntities();
        }

        public List<UsuarioDto> GetAllUsuarios()
        {
            using (var db = new productosDbEntities())
            {
                var lista = db.usuarios.Include(u => u.UsuarioRoles).ToList();
                return AutoMapperConfig.Mapper.Map<List<UsuarioDto>>(lista);
            }
        }

        public bool CreateUsuario(UsuarioDto usuarioDto)
        {
            try
            {
                using (var db = new productosDbEntities())
                {
                    var usuario = AutoMapperConfig.Mapper.Map<usuarios>(usuarioDto);
                    db.usuarios.Add(usuario);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public UsuarioDto Login(string user, string pass)
        {
            using (var db = new productosDbEntities())
            {
                var u = db.usuarios
                          .Include(x => x.UsuarioRoles.Select(r => r.roles))
                          .FirstOrDefault(x => x.username == user && x.password == pass && x.suspendido == false);

                return AutoMapperConfig.Mapper.Map<UsuarioDto>(u);
            }
        }
        public bool UpdateUsuario(int id, UsuarioDto usuarioDto)
        {
            try
            {
                using (var db = new productosDbEntities())
                {
                    // Buscamos el usuario incluyendo sus roles actuales
                    var usuarioExistente = db.usuarios
                                             .Include(u => u.UsuarioRoles)
                                             .FirstOrDefault(u => u.idUser == id);

                    if (usuarioExistente == null) return false;

                    AutoMapperConfig.Mapper.Map(usuarioDto, usuarioExistente);

                    
                    db.UsuarioRoles.RemoveRange(usuarioExistente.UsuarioRoles);

                    var tempUsuario = AutoMapperConfig.Mapper.Map<usuarios>(usuarioDto);
                    foreach (var rol in tempUsuario.UsuarioRoles)
                    {
                        usuarioExistente.UsuarioRoles.Add(rol);
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool DeleteUsuario(int id)
        {
            try
            {
                using (var db = new productosDbEntities())
                {
                    var usuarioExistente = db.usuarios.Find(id);

                    if (usuarioExistente != null)
                    {
                        var rolesDelUsuario = db.UsuarioRoles.Where(ur => ur.idUser == id).ToList();
                        db.UsuarioRoles.RemoveRange(rolesDelUsuario);

                        var perfilAsociado = db.perfilUsuario.FirstOrDefault(p => p.idUsuario == id);
                        if (perfilAsociado != null)
                        {
                           var direcciones = db.Direcciones.Where(d => d.idPerfilUsuario == perfilAsociado.idPerfilUsuario).ToList();
                            db.Direcciones.RemoveRange(direcciones);

                            var telefonos = db.Telefonos.Where(t => t.idPerfilUsuario == perfilAsociado.idPerfilUsuario).ToList();
                            db.Telefonos.RemoveRange(telefonos);

                            db.perfilUsuario.Remove(perfilAsociado);
                        }

                       
                        db.usuarios.Remove(usuarioExistente);
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
        }


    }
}