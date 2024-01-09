using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace PuntoDeVenta_API.Models
{
    public class ProductModel
    {
        public int product_id { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string? description { get; set; }
        [Required]
        public int? price { get; set; }
        [Required]
        public int? quantity { get; set; }

    }
}
