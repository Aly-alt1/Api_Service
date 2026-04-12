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

        public bool CreateDireccion(Api_Services_Rest.Dmain.Data.Direcciones direccion)
        {
            try
            {
                dbEntities.Direcciones.Add(direccion);
                dbEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateDireccion(int id, Api_Services_Rest.Dmain.Data.Direcciones direccionActualizada)
        {
            try
            {
                var existing = dbEntities.Direcciones.Find(id);
                if (existing == null) return false;

                existing.calle = direccionActualizada.calle;
                existing.colonia = direccionActualizada.colonia;
                existing.NumInterior = direccionActualizada.NumInterior;
                existing.NumExterior = direccionActualizada.NumExterior;
                existing.Municipio = direccionActualizada.Municipio;
                existing.idPerfilUsuario = direccionActualizada.idPerfilUsuario;

                dbEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteDireccion(int id)
        {
            try
            {
                var existing = dbEntities.Direcciones.Find(id);
                if (existing == null) return false;

                dbEntities.Direcciones.Remove(existing);
                dbEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }// fin de clase direccion service
}