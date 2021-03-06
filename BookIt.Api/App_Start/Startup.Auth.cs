﻿using System;
using System.Web.Http;

using BookIt.Api.Models;
using BookIt.Business.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using BookIt.Api.Providers;

namespace BookIt.Api
{
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "self";

            //UserManagerFactory = () => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            using (var s = GlobalConfiguration.Configuration.DependencyResolver.BeginScope())
            {
                var userManagerFactory = s.GetService(typeof(Func<UserManager<ApplicationUser>>)) as Func<UserManager<ApplicationUser>>;

                OAuthOptions = new OAuthAuthorizationServerOptions
                               {
                                   TokenEndpointPath = new PathString("/Token"),
                                   Provider = new ApplicationOAuthProvider(PublicClientId),
                                   AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                                   AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                                   AllowInsecureHttp = true
                               };
            }
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        //public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in ApplicationUser
            // and to use a cookie to temporarily store information about a ApplicationUser logging in with a third party login provider
            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Account/Login"),
                    Provider = new CookieAuthenticationProvider
                    {
                        OnApplyRedirect = ctx =>
                        {
                            if (!IsAjaxRequest(ctx.Request))
                            {
                                ctx.Response.Redirect(ctx.RedirectUri);
                            }
                        }
                    }
                });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication();
        }

        private static bool IsAjaxRequest(IOwinRequest request)
        {
            IReadableStringCollection query = request.Query;
            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }
            IHeaderDictionary headers = request.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }
    }
}
