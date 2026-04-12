using Api_Servixe.Mapper;
using Api_Servixe.Models.Dto;
using Api_Servixe.Service.Direccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_Servixe.Controllers.V1
{
    [RoutePrefix("api/v1/Direcciones")]
    public class DireccionesController : ApiController
    {
        private DireccionService _direccionService;

        public DireccionesController()
        {
            _direccionService = new DireccionService();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetDirecciones()
        {
            var dto = AutoMapperConfig.Mapper.Map<List<Models.Dto.DireccionDto>>(_direccionService.GetDirecciones());
            return Json(dto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetDireccionById(int id)
        {
            var direccion = AutoMapperConfig.Mapper.Map<Models.Dto.DireccionDto>(_direccionService.GetDireccionById(id));
            if (id <=0) 
            { 
                return BadRequest("El id debe ser mayor a 0");
            }
            if (direccion == null)
            {
                return NotFound();
            }

            return Json(direccion);
        }

        // Agregar a DireccionesController.cs
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateDireccion([FromBody] DireccionDto dto)
        {
            if (dto == null) return BadRequest("Los datos no pueden ser nulos");

            // Mapeo de DTO a Entidad de base de datos
            var entity = AutoMapperConfig.Mapper.Map<Api_Services_Rest.Dmain.Data.Direcciones>(dto);
            var result = _direccionService.CreateDireccion(entity);

            if (result) return Ok("Dirección registrada exitosamente");
            return BadRequest("No se pudo registrar la dirección");
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateDireccion(int id, [FromBody] DireccionDto dto)
        {
            if (dto == null) return BadRequest("Datos inválidos");

            var entity = AutoMapperConfig.Mapper.Map<Api_Services_Rest.Dmain.Data.Direcciones>(dto);
            var result = _direccionService.UpdateDireccion(id, entity);

            if (result) return Ok("Dirección actualizada correctamente");
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteDireccion(int id)
        {
            var result = _direccionService.DeleteDireccion(id);
            if (result) return Ok("Dirección eliminada correctamente");
            return NotFound();
        }
    }
}
