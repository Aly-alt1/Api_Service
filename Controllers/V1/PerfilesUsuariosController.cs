using Api_Servixe.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api_Servixe.Controllers
{
    [RoutePrefix("api/v1/PerfilUsuarios")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PerfilesUsuariosController : ApiController
    {
        private Service.PerfilUsuario.PerfilUsuarioService _perfilUsuarioService = null;

        public PerfilesUsuariosController()
        {
            _perfilUsuarioService = new Service.PerfilUsuario.PerfilUsuarioService();
        }

        [HttpGet]
        [Route("All")]
        public IHttpActionResult GetAllPerfilUsuarios()
        {
            try
            {
                var perfiles = AutoMapperConfig.Mapper.Map<List<Models.Dto.PerfilUsuariosDto>>(_perfilUsuarioService.GetAllPerfilUsuarios());
                return Json(perfiles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetPerfilUsuarioById(int id)
        {
            try
            {
                var perfilUsuario = AutoMapperConfig.Mapper.Map<Models.Dto.PerfilUsuariosDto>(_perfilUsuarioService.GetPerfilUsuarioById(id));
                if (perfilUsuario == null)
                {
                    return NotFound();
                }
                return Json(perfilUsuario);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreatePerfil([FromBody] Models.Dto.PerfilUsuariosDto perfilDto)
        {
            try
            {
                Api_Services_Rest.Dmain.Data.perfilUsuario perfil = new Api_Services_Rest.Dmain.Data.perfilUsuario();
                AutoMapperConfig.Mapper.Map(perfilDto, perfil);

                // Llamamos al servicio para guardar
                var result = _perfilUsuarioService.CreatePerfilUsuario(perfil);

                if (result)
                {
                    return Ok("Perfil de usuario creado exitosamente (incluyendo dirección y teléfono).");
                }
                else
                {
                    return BadRequest("No se pudo crear el perfil de usuario.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdatePerfil(int id, [FromBody] Models.Dto.PerfilUsuariosDto perfilDto)
        {
            try
            {
                Api_Services_Rest.Dmain.Data.perfilUsuario perfil = new Api_Services_Rest.Dmain.Data.perfilUsuario();
                AutoMapperConfig.Mapper.Map(perfilDto, perfil);

                var result = _perfilUsuarioService.UpdatePerfilUsuario(id, perfil);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeletePerfil(int id)
        {
            try
            {
                var result = _perfilUsuarioService.DeletePerfilUsuario(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }// fin de clase perfil usuarios
}
