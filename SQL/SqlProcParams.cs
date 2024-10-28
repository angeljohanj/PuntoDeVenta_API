namespace PuntoDeVenta_API.SQL
{
    public class SqlProcParams
    {
        private readonly string[] _sqlParams = new string[]
        {
            "username","password","role","id"
        };

        public string UserNameParam { get => _sqlParams[0]; }
        public string IdParam { get => _sqlParams[3]; }
    }
}
