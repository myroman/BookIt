using Autofac;

using BookIt.Api.Controllers;

namespace BookIt.Api
{
    public class ApiRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WaitinglistHelper>().InstancePerWebApiRequest();
        }
    }
}