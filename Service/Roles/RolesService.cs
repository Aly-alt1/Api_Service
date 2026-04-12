using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Servixe.Service.Roles
{
    public class RolesService
    {
        private Api_Services_Rest.Dmain.Data.productosDbEntities dbEntities = null;

        public RolesService()
        {
            dbEntities = new Api_Services_Rest.Dmain.Data.productosDbEntities();
        }

        public bool CreateRole(Api_Services_Rest.Dmain.Data.roles role)
        {
            bool result = false;
            var transaction = dbEntities.Database.BeginTransaction();
            try
            {
                dbEntities.roles.Add(role);//para agregar un nuevo rol a la base de datos
                dbEntities.SaveChanges();//para el commit de la transaccion
                transaction.Commit();
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return result;
            }
        }

        //para actualizar un rol existente en la base de datos
        public bool UpdateRole(int id, Api_Services_Rest.Dmain.Data.roles role)
        {
            bool result = false;
            var transaction = dbEntities.Database.BeginTransaction();
            try
            {
                //buscar el rol a actualizar por su id
                var rol = dbEntities.roles.Find(id);
                if (rol == null)
                {
                    return result;//si el rol no existe, retornar false
                }
                if (rol.idRoles != id)
                {
                    return result;
                }
                    //actualizar las propiedades del rol
                    rol.strValor = role.strValor;
                    rol.strDescripcion = role.strDescripcion;
                    dbEntities.SaveChanges();//para el commit de la transaccion
                    transaction.Commit();
                    result = true;
                    return result;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return result;
            }
        }

        public bool DeleteRole(int id)
        {

            bool result = false;
            var transaction = dbEntities.Database.BeginTransaction();
            try
            {
                //buscar el rol a eliminar por su id
                var rol = dbEntities.roles.Include("UsuarioRoles").FirstOrDefault(r => r.idRoles == id);
                if (rol == null)
                {
                    return result;//si el rol no existe, retornar false
                }

                var usuarioRoles = rol.UsuarioRoles.ToList();

                if(usuarioRoles.Count >0)
                {
                    foreach(var item in usuarioRoles)
                    {
                        dbEntities.UsuarioRoles.Remove(item);//para eliminar las relaciones entre el rol y los usuarios
                    }
                }

                dbEntities.roles.Remove(rol);//para eliminar el rol de la base de datos
                dbEntities.SaveChanges();//para el commit de la transaccion
                transaction.Commit();
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return result;
            }
        }

        public List<Api_Services_Rest.Dmain.Data.roles> GetAllRoles()
        {
            return dbEntities.roles.ToList();
        }
         public Api_Services_Rest.Dmain.Data.roles GetRoleById(int id)
        {
            return dbEntities.roles.Find(id);
        }

        public Api_Services_Rest.Dmain.Data.roles GetRoleByNombre(string name)
        {
            return dbEntities.roles.FirstOrDefault(r => r.strValor == name);
        }   
    }// fin de clase roles service
}