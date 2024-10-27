using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace PuntoDeVenta_API.Models
{
    //todo: aprender estanadarizacion de respuestas pd: no se como se escribe estandarizacion

   //public class EmployeListResponseModel
   // {
   //     public List<EmployeeModel> Data { get; set; }
   //     public string Message { get; set; }
   //      public bool isSuccess { get; set; }
   //     public bool isError { get; set; }
    //}
    public class EmployeeModel
    {
        public int Id { get; set; }

        [Required] public string? Name { get; set; }
        [Required] public string? Lastname { get; set; }
        [Required] public string? Age { get; set; }
        [Required] public string? Cedula { get; set; }
        [Required] public string? Cel { get; set; }
        [Required] public string? Tel { get; set; }
        [Required] public string? Email { get; set; }
        [Required] public DateTime? Birthdate { get; set; }
        [Required] public string? Address { get; set; }
        [Required] public string? Role { get; set; }
        [Required] public string? Nationality { get; set; }
    }
}
