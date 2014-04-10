using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;

using BookIt.Api.Models;
using BookIt.Api.Providers;
using BookIt.Business.Models;
using BookIt.Business.RepositoriesImpl;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace BookIt.Api
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
            builder.Register(c =>
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var x = new UserManager<ApplicationUser>(userStore);
                return x;
            });
            builder.RegisterModule<RepositoriesRegistrationModule>();
            builder.RegisterModule<ApiRegistrationModule>();
            //builder.RegisterApiControllers();
            builder.RegisterAssemblyTypes(
                Assembly.GetExecutingAssembly())
                .Where(t =>
                    !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t))
                .InstancePerMatchingLifetimeScope(
                    AutofacWebApiDependencyResolver.ApiRequestTag);

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;

        }
    }
}
