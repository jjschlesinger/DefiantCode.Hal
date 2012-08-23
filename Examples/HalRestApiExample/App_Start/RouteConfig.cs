using System;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace HalRestApiExample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}