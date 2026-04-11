using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api_Servixe
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            /*Habilitar el cors
             * var cors = new EnableCorsAttribute("*", "*", "*");
             * o
             * var cors = new EnableCorsAttribute("http://localhost:4200", "*", "GET,POST,PUT,DELETE");
             * 
             * config.EnableCors(cors);
             */
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            //configuracion para que no se muestren los campos con valor null y evitar el error de referencia circular
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling
                = NullValueHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = ReferenceLoopHandling.Ignore;

            //ruta web api
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
