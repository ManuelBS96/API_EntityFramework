using API_EntityFramework.Validaciones;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_EntityFramework.Entidades
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; } = string.Empty;

        //public int menor { get; set; }
        //public int mayor { get; set; }

        //[Range(18, 30)]
        //[NotMapped]
        //public int Edad { get; set; }
        //[NotMapped]
        //[CreditCard]
        //public string TarjetaCredito { get; set; }
        //[NotMapped]
        //[Url]
        //public string Url { get; set; }

        public List<Libro>? Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var PrimerLetra = Nombre[0].ToString();


                if (PrimerLetra != PrimerLetra.ToUpper()) yield return new ValidationResult("La primera letra debe ser mayúscula");


            }

            //if (menor > mayor)
            //{

            //    yield return new ValidationResult("El valor no puede ser mayor al campo Mayor");


            //}
        }
    }
}
