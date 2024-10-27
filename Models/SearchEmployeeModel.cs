using System.ComponentModel.DataAnnotations;

namespace PuntoDeVenta_API.Models
{
    public class SearchEmployeeModel
    {
        
        public string? Name { get; set; }
       
        public string? Lastname { get; set; }
       
        public string? Age  { get; set; }
       
        public string? Cedula { get; set; } 
       
        public string? Email { get; set; }

        public string? Role { get; set; }

    }
}
