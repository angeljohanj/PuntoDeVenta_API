namespace PuntoDeVenta_API.SQL
{
    public class SqlProcParams
    {
        private readonly string[] _sqlParams = new string[]
        {
            "username","password","role","id"
        };

        public string Id { get => _sqlParams[3]; }
        public string Username { get => _sqlParams[0]; }
        public string Password { get => _sqlParams[1]; }
        public string Role { get => _sqlParams[2]; }
    }
}
