using PuntoDeVenta_API.ADMIN.Interfaces;

namespace PuntoDeVenta_API.SQL
{
    public class SqlProcedures : ISqlProcedures
    {
        private readonly string[] _sqlProcedures = new string[]
        {
            "sp_List"
        };

        public string GetListUsersProc()
        {
            return _sqlProcedures[0];
        }
    }
}
