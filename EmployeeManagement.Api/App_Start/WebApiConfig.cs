using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeManagement.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnableCors(new EnableCorsAttribute("*", "*", "GET,PUT,POST,DELETE"));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            
        }
    }
}
