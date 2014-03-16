using Autofac.Builder;
using Autofac.Integration.WebApi;

namespace BookIt.Api.App_Start
{
    public static class RegistrationExtensions
    {
        public static IRegistrationBuilder<T1, T2, T3> InstancePerWebApiRequest<T1, T2, T3>(this IRegistrationBuilder<T1, T2, T3> builder)
        {
            return builder.InstancePerMatchingLifetimeScope(AutofacWebApiDependencyResolver.ApiRequestTag);
        }
    }
}