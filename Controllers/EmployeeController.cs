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

        [HttpPost]
        [Route("/SearchEmp")]

        public JsonResult Search(string search)
        {
            var result = new List<EmployeeModel>();
            string procedure = "sp_SearchEmployee";
            var connection = new DataConnection();
            try
            {
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(procedure, conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlCmd.Parameters.AddWithValue("search", search);
                        //sqlCmd.Parameters.AddWithValue("name", search.Name);
                        //sqlCmd.Parameters.AddWithValue("lastname", search.Lastname);
                        //sqlCmd.Parameters.AddWithValue("age", search.Age);
                        //sqlCmd.Parameters.AddWithValue("cedula", search.Cedula);
                        //sqlCmd.Parameters.AddWithValue("email", search.Email);
                        //sqlCmd.Parameters.AddWithValue("role", search.Role);
                        using(var dReader = sqlCmd.ExecuteReader())
                        {
                            while (dReader.Read())
                            {
                                result.Add(new EmployeeModel
                                {
                                    Id = Convert.ToInt32(dReader["id"]),
                                    Name = dReader["name"].ToString(),
                                    Lastname = dReader["lastname"].ToString(),
                                    Age = dReader["age"].ToString(),
                                    Cedula = dReader["cedula"].ToString(),
                                    Cel = dReader["cel"].ToString(),
                                    Tel = dReader["tel"].ToString(),
                                    Email = dReader["email"].ToString(),
                                    Birthdate = Convert.ToDateTime(dReader["birthdate"]),
                                    Address = dReader["address"].ToString(),
                                    Role = dReader["role"].ToString(),
                                    Nationality = dReader["nationality"].ToString(),
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
                result = null;
            }
            return new JsonResult(result);
        }

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
                                Id= Convert.ToInt32(dReader["id"]),
                                Name = dReader["name"].ToString(),
                                Lastname = dReader["lastname"].ToString(),
                                Age = dReader["age"].ToString(),
                                Cedula = dReader["cedula"].ToString(),
                                Cel = dReader["cel"].ToString(),
                                Tel = dReader["tel"].ToString(),
                                Email = dReader["email"].ToString(),
                                Birthdate = Convert.ToDateTime(dReader["birthdate"]),
                                Address = dReader["address"].ToString(),
                                Role = dReader["role"].ToString(),
                                Nationality = dReader["nationality"].ToString(),
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
                                emp.Name = dReader["name"].ToString();
                                emp.Lastname = dReader["lastname"].ToString();
                                emp.Age = dReader["age"].ToString();
                                emp.Cedula = dReader["cedula"].ToString();
                                emp.Cel = dReader["cel"].ToString();
                                emp.Tel = dReader["tel"].ToString();
                                emp.Email = dReader["email"].ToString();
                                emp.Birthdate = Convert.ToDateTime(dReader["birthdate"]);
                                emp.Address = dReader["address"].ToString();
                                emp.Role = dReader["role"].ToString();
                                emp.Nationality = dReader["nationality"].ToString();
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
                        sqlCmd.Parameters.AddWithValue("name", newEmp.Name);
                        sqlCmd.Parameters.AddWithValue("lastname", newEmp.Lastname);
                        sqlCmd.Parameters.AddWithValue("age", newEmp.Age);
                        sqlCmd.Parameters.AddWithValue("cedula", newEmp.Cedula);
                        sqlCmd.Parameters.AddWithValue("cel", newEmp.Cel);
                        sqlCmd.Parameters.AddWithValue("tel", newEmp.Tel);
                        sqlCmd.Parameters.AddWithValue("email", newEmp.Email);
                        sqlCmd.Parameters.AddWithValue("birthdate", newEmp.Birthdate);
                        sqlCmd.Parameters.AddWithValue("address", newEmp.Address);
                        sqlCmd.Parameters.AddWithValue("role", newEmp.Role);
                        sqlCmd.Parameters.AddWithValue("nationality", newEmp.Nationality);
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
                        sqlCmd.Parameters.AddWithValue("id", oEmp.Id);
                        sqlCmd.Parameters.AddWithValue("name", oEmp.Name);
                        sqlCmd.Parameters.AddWithValue("lastname", oEmp.Lastname);
                        sqlCmd.Parameters.AddWithValue("age", oEmp.Age);
                        sqlCmd.Parameters.AddWithValue("cedula", oEmp.Cedula);
                        sqlCmd.Parameters.AddWithValue("cel", oEmp.Cel);
                        sqlCmd.Parameters.AddWithValue("tel", oEmp.Tel);
                        sqlCmd.Parameters.AddWithValue("email", oEmp.Email);
                        sqlCmd.Parameters.AddWithValue("birthdate", oEmp.Birthdate);
                        sqlCmd.Parameters.AddWithValue("address", oEmp.Address);
                        sqlCmd.Parameters.AddWithValue("role", oEmp.Role);
                        sqlCmd.Parameters.AddWithValue("nationality", oEmp.Nationality);
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
