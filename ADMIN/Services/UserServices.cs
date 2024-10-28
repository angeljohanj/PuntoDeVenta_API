using PuntoDeVenta_API.ADMIN.Interfaces;
using PuntoDeVenta_API.ADMIN.Models;
using PuntoDeVenta_API.Data;
using PuntoDeVenta_API.SQL;
using System.Data;
using System.Data.SqlClient;

namespace PuntoDeVenta_API.ADMIN.Services
{
    public class UserServices : IUserServices
    {
        private readonly DataConnection _Connection;
        private readonly SqlProcParams _sqlParams;
        private readonly SqlProcedures _sqlProcedures;

        public UserServices()
        {
            _Connection = new DataConnection();
            _sqlParams = new SqlProcParams();
            _sqlProcedures = new SqlProcedures();
        }
        public async Task<List<UserModel>> GetUsers()
        {
            var users = new List<UserModel>();
            try
            {
                using var conn = new SqlConnection(_Connection.GetString());
                using var sqlCmd = new SqlCommand(_sqlProcedures.ListUSersProc, conn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using var dReader = await sqlCmd.ExecuteReaderAsync();
                while (dReader.Read())
                {
                    users.Add(new UserModel
                    {
                        Id = Convert.ToInt32(dReader["id"]),
                        Username = dReader["username"].ToString(),
                        Role = dReader["role"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                users = null;
            }
            return users;
        }
        public async Task<UserModel> GetASingleUser(int id)
        {
            var user = new UserModel();
            try
            {
                using var conn = new SqlConnection(_Connection.GetString());
                using var sqlCmd = new SqlCommand(_sqlProcedures.GetAUserProc, conn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                sqlCmd.Parameters.AddWithValue(_sqlParams.Id, id);
                using var dReader = await sqlCmd.ExecuteReaderAsync();
                while (dReader.Read())
                {
                    user.Id = Convert.ToInt32(dReader["id"]);
                    user.Username = dReader["username"].ToString();
                    user.Role = dReader["role"].ToString();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                user = null;
            }

            return user;
        }

        public async Task<bool> DeleteAUser(int id)
        {
            var ans = false;
            try
            {
                using var conn = new SqlConnection(_Connection.GetString());
                using var sqlCmd = new SqlCommand(_sqlProcedures.DeleteUserProc, conn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                sqlCmd.Parameters.AddWithValue(_sqlParams.Id, id);
                int affectedRows = await sqlCmd.ExecuteNonQueryAsync();
                if (affectedRows > 0)
                {
                    ans = true;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                ans = false;
            }
            return ans;
        }
    }
}
