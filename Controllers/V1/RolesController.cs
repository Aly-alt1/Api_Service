using Api_Servixe.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api_Servixe.Controllers.V1
{
    [RoutePrefix("api/v1/Roles")]
    [EnableCors(origins: "*", headers: "*",methods:"*")]
    public class RolesController : ApiController
    {
        private Service.Roles.RolesService _rolesService;
        public RolesController()
        {
            _rolesService = new Service.Roles.RolesService();
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateRole([FromBody]Models.Dto.RolesDto roleDto)
        {
            try
            {
                Api_Services_Rest.Dmain.Data.roles rol = new Api_Services_Rest.Dmain.Data.roles();
                AutoMapperConfig.Mapper.Map(roleDto, rol);
                var result = _rolesService.CreateRole(rol);
                if (result)
                {
                    return Ok("Rol creado exitosamente.");
                }
                else
                {
                    return BadRequest("No se pudo crear el rol.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromUri] Models.Dto.RolesDto roleDto)
        {
            try
            {
                Api_Services_Rest.Dmain.Data.roles rol = new Api_Services_Rest.Dmain.Data.roles();
                AutoMapperConfig.Mapper.Map(roleDto, rol);
                var result = _rolesService.UpdateRole(id, rol);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]

        public IHttpActionResult Delete([FromUri]int id)
        {
            try
            {
                var result = _rolesService.DeleteRole(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route ("")]
        public IHttpActionResult GetAllRoles()
        {
            try
            {
                var roles = AutoMapperConfig.Mapper.Map<List<Models.Dto.RolesDto>>(_rolesService.GetAllRoles());
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
         [HttpGet]
         [Route("nombre/{valor}")]
            public IHttpActionResult GetRoleByNombre([FromUri]string valor)
            {
                try
                {
                    var result = _rolesService.GetRoleByNombre(valor);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
        }
    }// fin de clase roles controller
}
