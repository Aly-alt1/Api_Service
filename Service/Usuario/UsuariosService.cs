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

        public List<UsuarioDto> GetUsuariosByRole(string roleName)
        {
            using (var db = new productosDbEntities())
            {
                var usuariosConRol = db.usuarios
                    .Include(u => u.UsuarioRoles.Select(ur => ur.roles))
                    .Where(u => u.UsuarioRoles.Any(ur => ur.roles.strValor == roleName))
                    .ToList();
                return AutoMapperConfig.Mapper.Map<List<UsuarioDto>>(usuariosConRol);
            }
        }

        public bool CreateUsuario(UsuarioDto usuarioDto)
        {
            using (var db = new productosDbEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var usuario = AutoMapperConfig.Mapper.Map<usuarios>(usuarioDto);
                        db.usuarios.Add(usuario);
                        db.SaveChanges(); // Guardamos primero el usuario para obtener su ID

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
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
            using (var db = new productosDbEntities())
            {
                // Iniciamos la transacción para asegurar integridad
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var usuarioExistente = db.usuarios
                                                 .Include(u => u.UsuarioRoles)
                                                 .FirstOrDefault(u => u.idUser == id);

                        if (usuarioExistente == null) return false;

                        // Actualizamos datos básicos
                        AutoMapperConfig.Mapper.Map(usuarioDto, usuarioExistente);

                        // Limpiamos roles actuales
                        db.UsuarioRoles.RemoveRange(usuarioExistente.UsuarioRoles);

                        // Insertamos los nuevos roles desde el DTO
                        var tempUsuario = AutoMapperConfig.Mapper.Map<usuarios>(usuarioDto);
                        foreach (var rol in tempUsuario.UsuarioRoles)
                        {
                            rol.idUser = id; // Aseguramos que el ID coincida
                            db.UsuarioRoles.Add(rol);
                        }

                        db.SaveChanges();
                        transaction.Commit(); // Si todo sale bien, guardamos definitivamente
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Si hay error, deshacemos todo
                        return false;
                    }
                }
            }
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