using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Servixe.Models.Dto
{
    public class PerfilUsuariosDto
    {
        public int IdPerfilUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Rfc { get; set; }
        public int IdUsuario { get; set; }
        public List<DireccionDto> DireccionDto { get; set; }
        public List<TelefonoDto> TelefonoDto { get; set; } 
    }
}