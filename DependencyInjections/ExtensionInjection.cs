namespace PuntoDeVenta_API.DependencyInjections
{
    public static class ExtensionInjection
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddJwtService()
                .AddUserServices();
            return services;
        }
    }
}
