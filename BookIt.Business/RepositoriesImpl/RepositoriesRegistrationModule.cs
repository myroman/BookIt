using Autofac;

using BookIt.Business.Abstract;

namespace BookIt.Business.RepositoriesImpl
{
    public class RepositoriesRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WaitingListRepository>()
                .As<IWaitingListRepository>()
                .SingleInstance();

            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .SingleInstance();

            builder.RegisterType<HubResourceRepository>()
                .As<IHubResourceRepository>()
                .SingleInstance();
        }
    }
}