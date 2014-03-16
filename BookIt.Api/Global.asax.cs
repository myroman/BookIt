using System.Reflection;
using System.Web;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;

using BookIt.Api.App_Start;
using BookIt.Business.RepositoriesImpl;

namespace BookIt.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(Register);

            ////var builder = new ContainerBuilder();
            ////builder.RegisterModule<RepositoriesRegistrationModule>();
            ////builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            ////var container = builder.Build();

            ////var resolver = new AutofacWebApiDependencyResolver(container);
            ////GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var builder = new ContainerBuilder();
            builder.RegisterModule<RepositoriesRegistrationModule>();
            builder.RegisterModule<ApiRegistrationModule>();
            builder.RegisterAssemblyTypes(
                Assembly.GetExecutingAssembly())
                .Where(t =>
                       !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t))
                .InstancePerWebApiRequest();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
        }
    }
}