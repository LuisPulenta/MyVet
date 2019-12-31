using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyVet.Web.Data.Entities
{
    public class Pet
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Foto")]
        public string ImageUrl { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Race { get; set; }

        public Owner Owner { get; set; }

        public PetType PetType { get; set; }

        [Display(Name = "Fecha Nacim.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Born { get; set; }

        public string Remarks { get; set; }

        public ICollection<History> Histories { get; set; }
        public ICollection<Agenda> Agendas { get; set; }

        //TODO: replace the correct URL for the image
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? "noimage"//null
            : $"http://keypress.serveftp.net:88/MyVet{ImageUrl.Substring(1)}";

        [Display(Name = "Fecha Nacim.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime BornLocal => Born.ToLocalTime();
    }
}