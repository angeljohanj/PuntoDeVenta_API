using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoDeVenta_API.Data;
using PuntoDeVenta_API.Models;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PuntoDeVenta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public string[] Parameters = { "username","password","role","id" };
        public string[] Procedures = { "sp_Create", "sp_List", "sp_GetAUser", "sp_Edit", "sp_Delete", "sp_ValidateLogin" }; 
        private DataConnection connection = new DataConnection();

        [HttpPost][Route("/Create")]
        public JsonResult Create(UserModel user)
        {
            var ans = false;
            try
            {
                using (var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(Procedures[0], conn))
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
                    using( var sqlCmd = new SqlCommand(Procedures[1], conn))
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

        [HttpGet]
        [Route("/Get")]
        public JsonResult Get(int id)
        {
            var user = new UserModel();
            try
            {
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(Procedures[2], conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlCmd.Parameters.AddWithValue(Parameters[3], id);
                        using(var dReader = sqlCmd.ExecuteReader())
                        {
                            if (dReader.Read())
                            {
                                user.Id = Convert.ToInt32(dReader["id"]);
                                user.Username = dReader["username"].ToString();
                                user.Role = dReader["role"].ToString();                                    
                            }
                            dReader.Close();
                        }
                    }
                    conn.Close();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                user = null;
            }

            return new JsonResult(user);
        }

        [HttpPut]
        [Route("/Delete")]

        public JsonResult Delete(int id)
        {
            var ans = false;
            try
            {
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using(SqlCommand cmd = new SqlCommand(Procedures[4], conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.Parameters.AddWithValue(Parameters[3], id);
                        var affectedRows = cmd.ExecuteNonQuery();
                        ans = true;                     
                        
                    }
                    conn.Close();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                ans = false;
            }

            return new JsonResult(ans);
        }

        [HttpPost][Route("/ValidateLogin")]
        public async Task<JsonResult> ValidateLogin(UserModel credentials)
        {
            var user = new UserModel();
            try
            {
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(Procedures[5], conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlCmd.Parameters.AddWithValue(Parameters[0], credentials.Username);
                        sqlCmd.Parameters.AddWithValue(Parameters[1], credentials.Password);
                        sqlCmd.Parameters.AddWithValue(Parameters[2], credentials.Role);
                        using (var dReader = sqlCmd.ExecuteReader())
                        {
                            if (dReader.Read())
                            {
                                var test = dReader.Read;
                                user.Username = dReader["username"].ToString();
                                user.Role = dReader["role"].ToString();

                                var claims = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.Name, user.Username),
                                    new Claim(ClaimTypes.Role, user.Role)
                                };

                                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                                
                            }
                            else
                            {
                                user = null;
                            }

                            dReader.Close();
                        }
                    }
                    conn.Close();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                user = null;
            }

            return new JsonResult(user);
        }
    }
}
