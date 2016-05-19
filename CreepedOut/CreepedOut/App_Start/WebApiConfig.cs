using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace CreepedOut
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            // Web API configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<CreepyThing>("CreepyThings");
            builder.EntitySet<CreepyThingImage>("CreepyThingImages");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
       
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
