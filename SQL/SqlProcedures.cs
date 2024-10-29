namespace PuntoDeVenta_API.SQL
{
    public class SqlProcedures
    {
        private readonly string[] _sqlProcedures = new string[]
        {
            "sp_List","sp_GetAUser","sp_Delete","sp_Create","sp_Edit","sp_ValidateLogin"
        };

        public string ListUSersProc { get => _sqlProcedures[0]; }
        public string GetAUserProc { get => _sqlProcedures[1]; }
        public string DeleteUserProc { get => _sqlProcedures[2]; }
        public string CreateUserProc { get => _sqlProcedures[3]; }
        public string ValidateUserLoginProc { get => _sqlProcedures[4]; }
    }
}
