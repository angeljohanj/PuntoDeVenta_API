using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using PuntoDeVenta_API.Data;
using PuntoDeVenta_API.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;

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

        [HttpGet][Route("/GetAnEmployee")]

        public JsonResult GetEmployee (int id)
        {
            var emp = new EmployeeModel();
            try
            {
                var connection = new DataConnection();
                string procedure = "sp_GetEmployee";
                SqlDataReader dReader;
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(procedure, conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("id",id);
                        conn.Open();
                        using(dReader = sqlCmd.ExecuteReader())
                        {
                            if (dReader.Read())  
                            {
                                emp.name = dReader["name"].ToString();
                                emp.lastname = dReader["lastname"].ToString();
                                emp.age = dReader["age"].ToString();
                                emp.cedula = dReader["cedula"].ToString();
                                emp.cel = dReader["cel"].ToString();
                                emp.tel = dReader["tel"].ToString();
                                emp.email = dReader["email"].ToString();
                                emp.birthdate = Convert.ToDateTime(dReader["birthdate"]);
                                emp.address = dReader["address"].ToString();
                                emp.role = dReader["role"].ToString();
                                emp.nationality = dReader["nationality"].ToString();
                            }
                            else
                            {
                                emp = null;
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message, "No such employee");
                
            }

            return new JsonResult(emp);
        }

        [HttpPost][Route("/CreateEmployee")]
        public JsonResult CreateEmployee(EmployeeModel newEmp)
        {
            var ans = false;
            try
            {
                DataConnection connection = new DataConnection();
                string procedure = "sp_RegEmployee";
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using( var sqlCmd = new SqlCommand(procedure, conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("name", newEmp.name);
                        sqlCmd.Parameters.AddWithValue("lastname", newEmp.lastname);
                        sqlCmd.Parameters.AddWithValue("age", newEmp.age);
                        sqlCmd.Parameters.AddWithValue("cedula", newEmp.cedula);
                        sqlCmd.Parameters.AddWithValue("cel", newEmp.cel);
                        sqlCmd.Parameters.AddWithValue("tel", newEmp.tel);
                        sqlCmd.Parameters.AddWithValue("email", newEmp.email);
                        sqlCmd.Parameters.AddWithValue("birthdate", newEmp.birthdate);
                        sqlCmd.Parameters.AddWithValue("address", newEmp.address);
                        sqlCmd.Parameters.AddWithValue("role", newEmp.role);
                        sqlCmd.Parameters.AddWithValue("nationality", newEmp.nationality);
                        conn.Open();
                        var result= sqlCmd.ExecuteNonQuery();
                        Console.WriteLine(result + "rows were affected!");
                    }
                }
                ans = true;
            }catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                ans = false;
            }

            return new JsonResult(ans);
        }

        [HttpPut][Route("/EditEmployee")]

        public JsonResult EditEmployee(EmployeeModel oEmp)
        {
            var ans = false;
            try
            {
                var connection = new DataConnection();
                string procedure = "sp_EditEmployee";
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(procedure, conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlCmd.Parameters.AddWithValue("id", oEmp.id);
                        sqlCmd.Parameters.AddWithValue("name", oEmp.name);
                        sqlCmd.Parameters.AddWithValue("lastname", oEmp.lastname);
                        sqlCmd.Parameters.AddWithValue("age", oEmp.age);
                        sqlCmd.Parameters.AddWithValue("cedula", oEmp.cedula);
                        sqlCmd.Parameters.AddWithValue("cel", oEmp.cel);
                        sqlCmd.Parameters.AddWithValue("tel", oEmp.tel);
                        sqlCmd.Parameters.AddWithValue("email", oEmp.email);
                        sqlCmd.Parameters.AddWithValue("birthdate", oEmp.birthdate);
                        sqlCmd.Parameters.AddWithValue("address", oEmp.address);
                        sqlCmd.Parameters.AddWithValue("role", oEmp.role);
                        sqlCmd.Parameters.AddWithValue("nationality", oEmp.nationality);
                        sqlCmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                ans = true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ans = false;
            }

            return new JsonResult(ans);
        }

        [HttpPut][Route("/RemoveEmployee")]
        public JsonResult DeleteEmployee(int id)
        {
            var ans = false;
            try
            {
                var connection = new DataConnection();
                string procedure = "sp_DeleteEmployee";
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(procedure, conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("id", id);
                        conn.Open();
                        sqlCmd.ExecuteNonQuery();
                        ans = true;
                    }
                }
            }catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                ans = false;
            }

            return new JsonResult(ans);
        }
    }
}
