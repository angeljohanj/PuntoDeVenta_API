using System.ComponentModel.DataAnnotations;

namespace PuntoDeVenta_API.ADMIN.DTOs.Responses
{
    public class SendUsersDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
