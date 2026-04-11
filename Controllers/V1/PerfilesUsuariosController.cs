using Api_Servixe.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_Servixe.Controllers
{
    [RoutePrefix("api/v1/PerfilUsuarios")]
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

    }// fin de clase perfil usuarios
}
