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
    }
}
