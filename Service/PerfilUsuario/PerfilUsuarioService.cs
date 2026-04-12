using Api_Services_Rest.Dmain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Servixe.Service.PerfilUsuario
{
    public class PerfilUsuarioService
    {
        private Api_Services_Rest.Dmain.Data.productosDbEntities dbEntities = null;

        public PerfilUsuarioService()
        {
            dbEntities = new Api_Services_Rest.Dmain.Data.productosDbEntities();
        }

        public List<Api_Services_Rest.Dmain.Data.perfilUsuario> GetAllPerfilUsuarios()
        {
            try
            {
                return dbEntities.perfilUsuario.Include("Direcciones").Include("Telefonos").ToList();
            }
            catch (Exception ex)
            {
                return new List<Api_Services_Rest.Dmain.Data.perfilUsuario>();
            }
        }

        public Api_Services_Rest.Dmain.Data.perfilUsuario GetPerfilUsuarioById(int id)
        {
            try
            {
                return dbEntities.perfilUsuario.Include("Direcciones").Include("Telefonos").FirstOrDefault(p => p.idPerfilUsuario == id);
            }
            catch (Exception ex)
            {
                return new Api_Services_Rest.Dmain.Data.perfilUsuario();
            }
        }
        public bool CreatePerfilUsuario(perfilUsuario perfil)
        {
            try
            {
                using (var db = new productosDbEntities())
                {
                    db.perfilUsuario.Add(perfil);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdatePerfilUsuario(int id, perfilUsuario perfilActualizado)
        {
            try
            {
                using (var db = new productosDbEntities())
                {
                    var perfilExistente = db.perfilUsuario
                                            .Include("Direcciones")
                                            .Include("Telefonos")
                                            .FirstOrDefault(p => p.idPerfilUsuario == id);

                    if (perfilExistente != null)
                    {
                        // 1. Actualizamos los datos básicos del perfil
                        perfilExistente.nombre = perfilActualizado.nombre;
                        perfilExistente.apellidoPaterno = perfilActualizado.apellidoPaterno;
                        perfilExistente.apellidoMaterno = perfilActualizado.apellidoMaterno;
                        perfilExistente.fechaNacimiento = perfilActualizado.fechaNacimiento;
                        perfilExistente.rfc = perfilActualizado.rfc;
                        perfilExistente.idUsuario = perfilActualizado.idUsuario;

                        db.Direcciones.RemoveRange(perfilExistente.Direcciones);
                        db.Telefonos.RemoveRange(perfilExistente.Telefonos);

                        foreach (var direccion in perfilActualizado.Direcciones)
                        {
                            perfilExistente.Direcciones.Add(direccion);
                        }

                        foreach (var telefono in perfilActualizado.Telefonos)
                        {
                            perfilExistente.Telefonos.Add(telefono);
                        }

                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePerfilUsuario(int id)
        {
            try
            {
                using (var db = new productosDbEntities())
                {
                    var perfilExistente = db.perfilUsuario.Find(id);

                    if (perfilExistente != null)
                    {
                        var direcciones = db.Direcciones.Where(d => d.idPerfilUsuario == id).ToList();
                        db.Direcciones.RemoveRange(direcciones);

                        var telefonos = db.Telefonos.Where(t => t.idPerfilUsuario == id).ToList();
                        db.Telefonos.RemoveRange(telefonos);

                        db.perfilUsuario.Remove(perfilExistente);

                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }// fin de clase perfil usuario service
}