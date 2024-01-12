using System.ComponentModel.DataAnnotations;

namespace PuntoDeVenta_API.Models
{
    public class ClientModel
    {
        public  int Client_id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Tel { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Client_type { get; set; }

        public string? Client_notes { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Url { get; set; }


    }
}
