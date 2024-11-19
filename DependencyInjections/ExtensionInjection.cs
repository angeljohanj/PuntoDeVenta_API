namespace PuntoDeVenta_API.DependencyInjections
{
    public static class ExtensionInjection
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddJwtService()
                .AddUserServices()
                .AddNewtonSoftJsonService()
                .AddSwaggerServices()
                .AddControllers();
            return services;
        }
    }
}
