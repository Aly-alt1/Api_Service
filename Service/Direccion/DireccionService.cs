using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Api_Services_Rest.Dmain.Data;

namespace Api_Servixe.Service.Direccion
{
    public class DireccionService
    {
        private Api_Services_Rest.Dmain.Data.productosDbEntities dbEntities = null;

        public DireccionService()
        {
            dbEntities = new Api_Services_Rest.Dmain.Data.productosDbEntities();
        }

        public List<Api_Services_Rest.Dmain.Data.Direcciones> GetDirecciones()
        {
            try
            { 
                return dbEntities.Direcciones.Include("perfilUsuario").ToList();
            }
            catch (Exception ex)
            {
                return new List<Api_Services_Rest.Dmain.Data.Direcciones>();
            }
        }

        public Api_Services_Rest.Dmain.Data.Direcciones GetDireccionById(int id)
        {
            try
            {
                return dbEntities.Direcciones.Include("perfilUsuario.Telefonos").Where(p => p.idPerfilUsuario == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }// fin de clase direccion service
}