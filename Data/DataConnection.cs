using PuntoDeVenta_API.Interfaces;

namespace PuntoDeVenta_API.Data
{
    public class DataConnection : IDataConnection
    {
        private readonly string sqlString = string.Empty;
        public DataConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            sqlString = builder.GetSection("ConnectionStrings:SqlString").Value;
        }

        public string GetString()
        {
            return sqlString;
        }
    }
}
