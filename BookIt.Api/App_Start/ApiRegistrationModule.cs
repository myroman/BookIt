using Autofac;
using Autofac.Integration.WebApi;

using BookIt.Api.Controllers;

namespace BookIt.Api.App_Start
{
    public class ApiRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WaitinglistHelper>().InstancePerWebApiRequest();
        }
    }
}