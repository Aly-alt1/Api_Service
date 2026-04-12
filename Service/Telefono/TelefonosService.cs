using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api_Services_Rest.Dmain.Data;

namespace Api_Servixe.Service.Telefono
{
    public class TelefonosService
    {
        private Api_Services_Rest.Dmain.Data.productosDbEntities dbEntities = null;
        public TelefonosService()
        {
            dbEntities = new Api_Services_Rest.Dmain.Data.productosDbEntities();
        }// fin de constructor

        public Api_Services_Rest.Dmain.Data.Telefonos GetTelefonoById(int id)
        {
            try
            {
                return dbEntities.Telefonos.FirstOrDefault(p=> p.idTelefono == id);
            }
            catch (Exception ex)
            {
                return new Api_Services_Rest.Dmain.Data.Telefonos();
            }
        }// fin de metodo GetTelefonoById


        public Api_Services_Rest.Dmain.Data.Telefonos GetTelefonoByNumero(string numero)
        {
            try
            {
                return dbEntities.Telefonos.FirstOrDefault(p => p.celular.Equals(numero));
            }
            catch (Exception ex)
            {
                return new Api_Services_Rest.Dmain.Data.Telefonos();
            }
        }// fin de metodo GetTelefonoByNumero

        public Api_Services_Rest.Dmain.Data.Telefonos GetTelefonosByUser(int idUsuarioPerfil)
        {
            try
            {
                return dbEntities.Telefonos.Include("perfilUsuario").FirstOrDefault(p => p.idPerfilUsuario == idUsuarioPerfil);
            }
            catch (Exception ex)
            {
                return new Api_Services_Rest.Dmain.Data.Telefonos();
            }
        }// fin de metodo GetTelefonosByUser

    }// fin de clase telefonos dto
}