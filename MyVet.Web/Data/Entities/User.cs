using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyVet.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Document { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string LastName { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Address { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Nombre Completo y Documento")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        [DisplayFormat(DataFormatString = "{0:N6}")]
        public double Latitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:N6}")]
        public double Longitude { get; set; }

    }
}