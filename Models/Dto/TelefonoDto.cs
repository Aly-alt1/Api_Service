using Api_Services_Rest.Dmain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Servixe.Models.Dto
{
    public class TelefonoDto
    {
        public int IdTelefono { get; set; }
        public string Celular { get; set; }
        public string Casa { get; set; }
        public string Oficina { get; set; }
        public int IdPerfilUsuario { get; set; }

        public PerfilUsuariosDto PerfilUsuarioDto { get; set; }
    }
}