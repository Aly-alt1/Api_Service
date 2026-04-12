using Api_Servixe.Models.Dto;
using Api_Servixe.Service.UsuarioRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api_Servixe.Controllers.V1
{
    [RoutePrefix("api/v1/UsuarioRoles")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuariosRolesController : ApiController
    {
        private UsuarioRolesService _usuarioRolesService;

        public UsuariosRolesController()
        {
            _usuarioRolesService = new UsuarioRolesService();
        }

        // Obtener todos los roles asignados a un usuario
        [HttpGet]
        [Route("Usuario/{idUsuario:int}")]
        public IHttpActionResult GetRolesByUsuario(int idUsuario)
        {
            try
            {
                var roles = _usuarioRolesService.GetRolesByUsuario(idUsuario);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Asignar rol
        [HttpPost]
        [Route("Assign")]
        public IHttpActionResult AssignRole([FromBody] UsuarioRolesDto asignacionDto)
        {
            try
            {
                if (asignacionDto == null || asignacionDto.IdUser <= 0 || asignacionDto.IdRoles <= 0)
                {
                    return BadRequest("Datos de asignación inválidos.");
                }

                var result = _usuarioRolesService.AssignRoleToUser(asignacionDto);
                if (result) return Ok("Rol asignado al usuario correctamente.");

                return BadRequest("No se pudo asignar el rol. Es posible que el usuario ya lo tenga.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Revocar rol
        [HttpDelete]
        [Route("Remove/User/{idUser:int}/Role/{idRole:int}")]
        public IHttpActionResult RemoveRole(int idUser, int idRole)
        {
            try
            {
                var result = _usuarioRolesService.RemoveRoleFromUser(idUser, idRole);
                if (result) return Ok("Rol revocado exitosamente.");

                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
