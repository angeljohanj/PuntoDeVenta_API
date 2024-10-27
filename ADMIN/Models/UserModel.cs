using System.ComponentModel.DataAnnotations;

namespace PuntoDeVenta_API.ADMIN.Models
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
        [Required]
        public DateTime? Created_at { get; set; }
        [Required]
        public DateTime? Modified_at { get; set; }
        [Required]
        public DateTime? Deleted_at { get; set; }
        [Required]
        public string? Deleted { get; set; }
        [Required]
        public string? Delted_by { get; set; }
        [Required]
        public string? Edited_by { get; set; }
    }
}
