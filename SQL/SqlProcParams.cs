using PuntoDeVenta_API.ADMIN.Interfaces;

namespace PuntoDeVenta_API.SQL
{
    public class SqlProcParams : ISqlProcParams
    {
        private readonly string[] _sqlParams = new string[]
        {
            "username","password","role","id"
        };

        public string GetUserNameParam()
        {
            return _sqlParams[0];
        }

    }
}
