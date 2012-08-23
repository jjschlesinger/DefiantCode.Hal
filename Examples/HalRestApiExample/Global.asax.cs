using System;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;
using DefiantCode.Hal.WebApi.Formatters.HalJson;

namespace HalRestApiExample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.Formatters.Add(new HalJsonFormatter());
        }
    }
}