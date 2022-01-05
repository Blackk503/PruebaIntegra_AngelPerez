using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AngelPerezIntegra.DTO
{
    /// <summary>Clase <c>DTOEmpleado</c> 
    /// Valida del lado del cliente: 
    /// Formato del correo y campo de foto IFormFile
    /// .</summary>
    public class DTOEmpleado
    {
        public int IdEmpleado { get; set; }        
        [Required(ErrorMessage = "Debe ingresar el apellido del empleado")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del empleado")]
        public string Nombre { get; set; }

        [DisplayName("Teléfono")]
        [Required(ErrorMessage = "Debe ingresar el número de teléfono del empleado")]    
        [RegularExpression(@"^((\+0?1\s)?)\(?\d{3}\)?[\s.\s]\d{3}[\s.-]\d{4}$", ErrorMessage = "Ingrese un numero de telefono valido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Debe ingresar correo electrónico del empleado")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electronico valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar una foto del empleado")]
        public IFormFile Foto { get; set; }

        [DisplayName("Fecha de contratación")]
        [Required(ErrorMessage = "Debe ingresar la fecha de contratación del empleado")]
        [DataType(DataType.DateTime, ErrorMessage = "Valor invalido")]
        //Formato solicitado por el cliente
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaContratacion { get; set; }
        public string FechaFormat { get; set; }

        public string RutaFoto { get; set; }
    }
}
