using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoDeVenta_API.Data;
using PuntoDeVenta_API.Models;
using System.Data.SqlClient;
using System.Data;

namespace PuntoDeVenta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public string[] Parameters = { "username","password","role" };
        public string[] procedures = { "sp_Create", "sp_List", "sp_GetAUser", "sp_Edit", "sp_Delete" }; 
        private DataConnection connection = new DataConnection();

        [HttpPost][Route("/Create")]
        public JsonResult Create(UserModel user)
        {
            var ans = false;
            try
            {
                using (var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(procedures[0], conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlCmd.Parameters.AddWithValue(Parameters[0], user.Username);
                        sqlCmd.Parameters.AddWithValue(Parameters[1], user.Password);
                        sqlCmd.Parameters.AddWithValue(Parameters[2], user.Role);
                        sqlCmd.ExecuteNonQuery();
                        ans = true;
                    }
                    conn.Close();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ans = false;
            }

            return new JsonResult(ans);
        }

        [HttpGet][Route("/List")]
        public JsonResult List()
        {
            var users = new List<UserModel>();
            try
            {
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using( var sqlCmd = new SqlCommand(procedures[1], conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        using(var dReader = sqlCmd.ExecuteReader())
                        {
                            while (dReader.Read())
                            {
                                users.Add(new UserModel()
                                {
                                    Id = Convert.ToInt32(dReader["id"]),
                                    Username = dReader["username"].ToString(),
                                    Password = dReader["password"].ToString(),
                                    Role = dReader["role"].ToString()
                                });
                            }
                            dReader.Close();
                        }
                    }
                    conn.Close();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                users = null;
            }

            return new JsonResult(users);
        }
    }
}
