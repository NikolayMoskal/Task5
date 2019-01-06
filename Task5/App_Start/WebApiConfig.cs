using System.Web.Http;
using System.Web.Http.Cors;

namespace Task5
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("http://localhost:4200", "*", "*"));
            config.Formatters.Add(new CustomJsonFormatter());
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{action}",
                new {controller = "Home", action = "Index"}
            );
            config.Routes.MapHttpRoute(
                "IndexPath",
                "",
                new {controller = "Home", action = "Index"}
            );
        }
    }
}