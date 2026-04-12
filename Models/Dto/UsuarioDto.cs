using Api_Services_Rest.Dmain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Servixe.Models.Dto
{
    public class UsuarioDto
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Suspendido { get; set; }

        //public List<PerfilUsuariosDto> PerfilUsuariosDto { get; set; }
        public List<UsuarioRolesDto> UsuarioRolDto { get; set; }
    }
}