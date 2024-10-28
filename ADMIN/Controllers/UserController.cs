using Mapster;
using Microsoft.AspNetCore.Mvc;
using PuntoDeVenta_API.ADMIN.DTOs.Responses;
using PuntoDeVenta_API.ADMIN.Models;
using PuntoDeVenta_API.ADMIN.Services;

namespace PuntoDeVenta_API.ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;
        public UserController()
        {
            _userServices = new UserServices();
        }
        /*[HttpPost]
        [Route("/Create")]
        public JsonResult Create(UserModel user)
        {
            var ans = false;
            try
            {
                using (var conn = new SqlConnection(connection.GetString()))
                {
                    using (var sqlCmd = new SqlCommand(Procedures[0], conn))
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ans = false;
            }

            return new JsonResult(ans);
        }*/

        [HttpGet]
        [Route("/List")]
        public async Task<JsonResult> List()
        {
            List<UserModel> users = new List<UserModel>();
            try
            {
                users = await _userServices.GetUsers();
                if (ModelState.IsValid)
                {
                    var oUsers = users.Adapt<List<SendUsersDTO>>();
                    return new JsonResult(oUsers);
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult(users);
        }

        [HttpGet]
        [Route("/Get/{id}")]
        public async Task<JsonResult> Get([FromRoute] int id)
        {
            var user = await _userServices.GetASingleUser(id);
            try
            {
                if (ModelState.IsValid)
                {
                    var oUser = user.Adapt<SendUsersDTO>();
                    return new JsonResult(oUser);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                user = null;
            }
            return new JsonResult(user);
        }

        /*[HttpPut]
        [Route("/Delete")]

        public JsonResult Delete(int id)
        {
            var ans = false;
            try
            {
                using (var conn = new SqlConnection(connection.GetString()))
                {
                    using (SqlCommand cmd = new SqlCommand(Procedures[4], conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.Parameters.AddWithValue(Parameters[3], id);
                        var affectedRows = cmd.ExecuteNonQuery();
                        ans = true;

                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ans = false;
            }

            return new JsonResult(ans);
        }*/

        /*[HttpPost]
        [Route("/ValidateLogin")]
        public async Task<JsonResult> ValidateLogin(UserModel credentials)
        {
            var user = new UserModel();
            try
            {
                using (var conn = new SqlConnection(connection.GetString()))
                {
                    using (var sqlCmd = new SqlCommand(Procedures[5], conn))
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                user = null;
            }

            return new JsonResult(user);
        }*/
    }
}
