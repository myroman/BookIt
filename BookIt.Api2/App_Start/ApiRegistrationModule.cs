using Autofac;

using BookIt.Api2.Controllers;

namespace BookIt.Api2
{
    public class ApiRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WaitinglistHelper>().InstancePerWebApiRequest();
        }
    }
}