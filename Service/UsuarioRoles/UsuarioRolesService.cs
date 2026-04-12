using Api_Services_Rest.Dmain.Data;
using Api_Servixe.Mapper;
using Api_Servixe.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Api_Servixe.Service.UsuarioRoles
{
    public class UsuarioRolesService
    {
        private Api_Services_Rest.Dmain.Data.productosDbEntities dbEntities = null;
        public UsuarioRolesService()
        {
            dbEntities = new Api_Services_Rest.Dmain.Data.productosDbEntities();
        }
        public List<UsuarioRolesDto> GetRolesByUsuario(int idUsuario)
        {
            using (var db = new productosDbEntities())
            {
                var lista = db.UsuarioRoles.Include(ur => ur.roles).Where(ur => ur.idUser == idUsuario).ToList();
                return AutoMapperConfig.Mapper.Map<List<UsuarioRolesDto>>(lista);
            }
        }

        // Asignar un nuevo rol a un usuario
        public bool AssignRoleToUser(UsuarioRolesDto asignacionDto)
        {
            try
            {
                using (var db = new productosDbEntities())
                {
                    // Validar si el usuario ya tiene ese rol para no duplicarlo
                    var existe = db.UsuarioRoles.Any(ur => ur.idUser == asignacionDto.IdUser && ur.idRoles == asignacionDto.IdRoles);
                    if (existe) return false;

                    var nuevaAsignacion = AutoMapperConfig.Mapper.Map<Api_Services_Rest.Dmain.Data.UsuarioRoles>(asignacionDto);
                    db.UsuarioRoles.Add(nuevaAsignacion);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        // Revocar (Eliminar) un rol de un usuario
        public bool RemoveRoleFromUser(int idUser, int idRole)
        {
            try
            {
                using (var db = new productosDbEntities())
                {
                    var asignacion = db.UsuarioRoles
                                       .FirstOrDefault(ur => ur.idUser == idUser && ur.idRoles == idRole);

                    if (asignacion != null)
                    {
                        db.UsuarioRoles.Remove(asignacion);
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