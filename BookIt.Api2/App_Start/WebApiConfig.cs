using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

using BookIt.Business.RepositoriesImpl;

using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace BookIt.Api2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var builder = new ContainerBuilder();
            builder.RegisterModule<RepositoriesRegistrationModule>();
            builder.RegisterModule<ApiRegistrationModule>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
