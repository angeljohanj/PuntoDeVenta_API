using Newtonsoft.Json.Serialization;

namespace PuntoDeVenta_API.DependencyInjections
{
    public static class NewtonSoftServiceInjection
    {
        public static IServiceCollection AddNewtonSoftJsonService(this IServiceCollection services)
        {

            services.AddControllers()
                .AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(option => option.SerializerSettings.ContractResolver = new DefaultContractResolver());

            return services;
        }
    }
}

