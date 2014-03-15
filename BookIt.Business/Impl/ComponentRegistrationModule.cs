using Autofac;

using BookIt.Business.Abstract;

namespace BookIt.Business.Impl
{
    public class ComponentRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .SingleInstance();

            builder.RegisterType<HubResourceRepository>()
                .As<IHubResourceRepository>()
                .SingleInstance();
        }
    }
}