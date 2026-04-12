using Api_Servixe.Mapper;
using Api_Servixe.Service.Telefono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_Servixe.Controllers.V1
{
    [RoutePrefix("api/v1/Telefonos")]
    public class TelefonosController : ApiController
    {
        private TelefonosService _telefonosService;
        public TelefonosController()
        {
            _telefonosService = new TelefonosService();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetTelefonoById(int id)
        {
            var telefonos =AutoMapperConfig.Mapper.Map<Api_Servixe.Models.Dto.TelefonoDto> (_telefonosService.GetTelefonoById(id));

            return Json(telefonos);
        }

        [HttpGet]
        [Route("numero/{celular}")]
        public IHttpActionResult GetTelefonoByNumero(string celular)
        {
            var telefonos = AutoMapperConfig.Mapper.Map<Api_Servixe.Models.Dto.TelefonoDto>(_telefonosService.GetTelefonoByNumero(celular));
            return Json(telefonos);
        }
    }// fin de clase telefonos controller
}
