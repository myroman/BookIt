using System;

using Autofac;

using BookIt.Api.Controllers;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookIt.Api
{
    public class ApiRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WaitinglistHelper>().InstancePerWebApiRequest();
            //builder.RegisterType<Func<UserManager<IdentityUser>>>().SingleInstance();
        }
    }
}