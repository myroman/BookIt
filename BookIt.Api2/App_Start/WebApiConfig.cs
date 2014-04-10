﻿using System.Reflection;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;

using BookIt.Business.RepositoriesImpl;

using Microsoft.Owin.Security.OAuth;

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