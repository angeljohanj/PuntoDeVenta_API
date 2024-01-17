using System.ComponentModel.DataAnnotations;

namespace PuntoDeVenta_API.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
