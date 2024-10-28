using PuntoDeVenta_API.ADMIN.Interfaces.SqlInterfaces;

namespace PuntoDeVenta_API.SQL
{
    public class SqlProcedures : ISqlUserProcedures
    {
        private readonly string[] _sqlProcedures = new string[]
        {
            "sp_List","sp_GetAUser"
        };

        public string GetListUsersProc()
        {
            return _sqlProcedures[0];
        }

        public string GetFetchAUserProc()
        {
            return _sqlProcedures[1];
        }
    }
}
