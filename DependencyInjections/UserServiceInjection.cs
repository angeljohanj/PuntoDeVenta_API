using PuntoDeVenta_API.ADMIN.Interfaces;
using PuntoDeVenta_API.ADMIN.Services;

namespace PuntoDeVenta_API.DependencyInjections
{
    public static class UserServiceInjection
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
            return services;
        }
    }
}
