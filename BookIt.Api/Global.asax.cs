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
            GlobalConfiguration.Configure(WebApiConfig.RegisterRoutes);
            GlobalConfiguration.Configure(RegisterAutofacStuff);
        }

        public static void RegisterAutofacStuff(HttpConfiguration config)
        {
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