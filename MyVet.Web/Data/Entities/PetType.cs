using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MyVet.Web.Data.Entities
{
    public class PetType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo Mascota")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}