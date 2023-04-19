using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Mis_Management_system
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // we need this basically to connect cross orgin from different orgin and if we try to connect with backend api example react vue angular or other orgin we are able to connect this 
            // with this single of line but if this two line of code are not there trust me byagorana it should give error on cross orgin 
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
