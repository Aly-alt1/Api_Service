using Api_Servixe.Models.Dto;
using Api_Servixe.Service.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api_Servixe.Controllers.V1
{
    [RoutePrefix("api/v1/Usuarios")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class UsuariosController : ApiController
    {
        private UsuariosService _usuariosService;

            public UsuariosController()
            {
                _usuariosService = new UsuariosService();
            }

            // 1. OBTENER TODOS LOS USUARIOS
            [HttpGet]
            [Route("All")]
            public IHttpActionResult GetAllUsuarios()
            {
                try
                {
                    var usuarios = _usuariosService.GetAllUsuarios();
                    return Ok(usuarios);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

        [HttpGet]
        [Route("nombre/{valor}")]
        public IHttpActionResult GetUsuariosByName(string valor)
        {
            try
            {
                var usuarios = _usuariosService.GetUsuariosByRole(valor);
                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound();
                }
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // 2. CREAR USUARIO (Y SUS ROLES)
        [HttpPost]
            [Route("Create")]
            public IHttpActionResult CreateUsuario([FromBody] UsuarioDto usuarioDto)
            {
                try
                {
                    if (usuarioDto == null) return BadRequest("Los datos del usuario no pueden ser nulos.");

                    var result = _usuariosService.CreateUsuario(usuarioDto);

                    if (result)
                    {
                        return Ok("Usuario y sus roles creados exitosamente.");
                    }
                    return BadRequest("No se pudo crear el usuario.");
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            // 3. ACTUALIZAR USUARIO
            [HttpPut]
            [Route("{id:int}")]
            public IHttpActionResult UpdateUsuario(int id, [FromBody] UsuarioDto usuarioDto)
            {
                try
                {
                    if (usuarioDto == null) return BadRequest("Datos nulos.");

                    // Asumiendo que agregaste el método UpdateUsuario en tu UsuariosService 
                    // con la misma lógica que hicimos para PerfilUsuarioService
                    var result = _usuariosService.UpdateUsuario(id, usuarioDto);

                    if (result) return Ok("Usuario actualizado correctamente.");
                    return NotFound();
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            // 4. ELIMINAR USUARIO
            [HttpDelete]
            [Route("{id:int}")]
            public IHttpActionResult DeleteUsuario(int id)
            {
                try
                {
                    var result = _usuariosService.DeleteUsuario(id);
                    if (result) return Ok("Usuario eliminado.");
                    return NotFound();
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            // 5. LOGIN DE ACCESO
            [HttpPost]
            [Route("Login")]
            public IHttpActionResult Login([FromBody] UsuarioDto loginInfo)
            {
                try
                {
                    // Validamos que vengan los datos requeridos
                    if (loginInfo == null || string.IsNullOrEmpty(loginInfo.Username) || string.IsNullOrEmpty(loginInfo.Password))
                    {
                        return BadRequest("Debe proporcionar usuario y contraseña.");
                    }

                    // Llamamos al método Login de tu servicio que creamos en el paso anterior
                    var usuario = _usuariosService.Login(loginInfo.Username, loginInfo.Password);

                    if (usuario == null)
                    {
                        // Si retorna null, significa que no existe, la contraseña es incorrecta, o está suspendido
                        return Unauthorized();
                    }

                    // Si el acceso es correcto, devolvemos un mensaje de éxito junto con los datos del usuario (y sus roles)
                    return Ok(new
                    {
                        Mensaje = "Acceso concedido",
                        Usuario = usuario
                    });
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        
    }
}
