namespace PuntoDeVenta_API.DependencyInjections
{
    public static class SwaggerServiceInjection
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer().AddSwaggerGen();
            return services;
        }
    }
}
