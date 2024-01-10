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
    public class ProductController : ControllerBase
    {
        [HttpGet][Route("/ListProducts")]

        public JsonResult ListProducts()
        {
            var products = new List<ProductModel>();
            try
            {
                var connection = new DataConnection();
                string procedure = "sp_ListProducts";
                SqlDataReader dReader;
                using(var conn = new SqlConnection(connection.GetString()))
                {
                    using(var sqlCmd = new SqlCommand(procedure, conn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        dReader = sqlCmd.ExecuteReader();
                        while (dReader.Read())
                        {
                            products.Add(new ProductModel()
                            {
                                Product_id = Convert.ToInt32(dReader["product_id"]),
                                Name = dReader["name"].ToString(),
                                Description = dReader["description"].ToString(),
                                Price =Convert.ToInt32( dReader["price"]),
                                Quantity =Convert.ToInt32( dReader["quantity"])

                            }); 
                        }
                        dReader.Close();
                    }
                    conn.Close();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                products = null;
            }

            return new JsonResult(products);
        }

    }
}
