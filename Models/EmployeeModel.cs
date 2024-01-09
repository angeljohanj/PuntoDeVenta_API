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
        public int id { get; set; }

        [Required] public string? name { get; set; }
        [Required] public string? lastname { get; set; }
        [Required] public string? age { get; set; }
        [Required] public string? cedula { get; set; }
        [Required] public string? cel { get; set; }
        [Required] public string? tel { get; set; }
        [Required] public string? email { get; set; }
        [Required] public DateTime? birthdate { get; set; }
        [Required] public string? address { get; set; }
        [Required] public string? role { get; set; }
        [Required] public string? nationality { get; set; }
    }
}
