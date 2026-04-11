using Api_Services_Rest.Dmain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Servixe.Models.Dto
{
    public class UsuarioRolesDto
    {
        public int IdUsuarioRoles { get; set; }
        public int IdUser { get; set; }
        public int IdRoles { get; set; }

        public RolesDto RolesDto { get; set; }
        public UsuarioDto UsuariosDto { get; set; }
    }
}