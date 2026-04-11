using Api_Services_Rest.Dmain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Servixe.Models.Dto
{
    public class RolesDto
    {

        public int IdRoles { get; set; }
        public string StrValor { get; set; }
        public string StrDescripcion { get; set; }
        //List<UsuarioRolesDto> UsuarioRolDto { get; set; }
    }
}