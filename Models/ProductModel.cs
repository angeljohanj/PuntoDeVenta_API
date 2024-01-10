using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace PuntoDeVenta_API.Models
{
    public class ProductModel
    {
        public int Product_id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
