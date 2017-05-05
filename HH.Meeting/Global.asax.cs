using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace HH.Meeting
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            // Initialize dependency injection
            var container = new Container();
            new SimpleInjectorContainer().Initialize(container);


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
    }
}