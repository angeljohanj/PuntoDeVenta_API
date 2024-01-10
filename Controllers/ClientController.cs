using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoDeVenta_API.Data;
using PuntoDeVenta_API.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Xml.Linq;
namespace PuntoDeVenta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet][Route("/GetClients")]
        public JsonResult GetClients()
        {
            var clients = new List<ClientModel>();
            try
            {
                var connection = new DataConnection();
                string procedure = "sp_ListClients";
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
                            clients.Add(new ClientModel()
                            {
                                Client_id = Convert.ToInt32(dReader["client_id"]),
                                Name = dReader["name"].ToString(),
                                Tel = dReader["tel"].ToString(),
                                Address = dReader["address"].ToString(),
                                Client_type = dReader["client_type"].ToString(),
                                Client_notes = dReader["client_notes"].ToString(),
                                Email = dReader["email"].ToString(),
                                Url = dReader["url"].ToString(),
                            });
                        }
                        dReader.Close();
                    }
                    conn.Close();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                clients = null;
            }

            return new JsonResult(clients);
        }

        [HttpGet][Route("/GetAClient")]
        public JsonResult GetAnClient (int id)
        {
            var client = new ClientModel();
            try
            {
                DataConnection connection = new DataConnection();
                string procedure = "sp_GetClient";
                SqlDataReader dReader;
                using(SqlConnection conn = new SqlConnection(connection.GetString()))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(procedure, conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlCmd.Parameters.AddWithValue("id", id);
                        using(dReader= sqlCmd.ExecuteReader())
                        {
                            if (dReader.Read())
                            {
                                client.Client_id = Convert.ToInt32(dReader["client_id"]);
                                client.Name = dReader["name"].ToString();
                                client.Tel = dReader["tel"].ToString();
                                client.Address = dReader["address"].ToString();
                                client.Client_type = dReader["client_type"].ToString();
                                client.Client_notes = dReader["client_notes"].ToString();
                                client.Email = dReader["email"].ToString();
                                client.Url = dReader["url"].ToString();
                            }
                            dReader.Close();
                        }
                    }
                    conn.Close();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                client = null;
            }

            return new JsonResult(client);
        }

        [HttpPost][Route("/CreateClient")]
        public JsonResult CreateClient(ClientModel client)
        {
            bool ans = false;
            try
            {
                DataConnection connection = new DataConnection();
                string procedure = "sp_RegClient";
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(procedure, conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlCmd.Parameters.AddWithValue("name", client.Name);
                        sqlCmd.Parameters.AddWithValue("tel", client.Tel);
                        sqlCmd.Parameters.AddWithValue("address", client.Address);
                        sqlCmd.Parameters.AddWithValue("client_type", client.Client_type);
                        sqlCmd.Parameters.AddWithValue("email", client.Email);
                        sqlCmd.Parameters.AddWithValue("url", client.Url);
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
    }
}
