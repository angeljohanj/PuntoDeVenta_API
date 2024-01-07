using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using PuntoDeVenta_API.Data;
using PuntoDeVenta_API.Models;

namespace PuntoDeVenta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        [HttpGet][Route("/ListEmployees")]

        public JsonResult ListEmployees()
        {
            var emps = new List<EmployeeModel>();
            try
            {
                
                string procedure = "sp_ListEmployees";
                var connection = new DataConnection();
                SqlDataReader dReader;
                using(SqlConnection conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(procedure, conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        dReader = sqlCmd.ExecuteReader();
                        while (dReader.Read())
                        {
                            emps.Add(new EmployeeModel()
                            {
                                id = Convert.ToInt32(dReader["id"]),
                                name = dReader["name"].ToString(),
                                lastname = dReader["lastname"].ToString(),
                                age = dReader["age"].ToString(),
                                cedula = dReader["cedula"].ToString(),
                                cel = dReader["cel"].ToString(),
                                tel = dReader["tel"].ToString(),
                                email = dReader["email"].ToString(),
                                birthdate = Convert.ToDateTime(dReader["birthdate"]),
                                address = dReader["address"].ToString(),
                                role = dReader["role"].ToString(),
                                nationality = dReader["nationality"].ToString(),
                            });
                        }
                        conn.Close();
                        dReader.Close();
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                emps = null;
            }

            return new JsonResult(emps);
        }
    }
}
