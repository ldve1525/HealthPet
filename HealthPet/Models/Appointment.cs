using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HealthPet.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        public string Categorie { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un turno")]
        [Display(Name = "Horario")]
        public int Shift { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required]
        [MinLength(9)]
        [Display(Name = "Cédula")]
        public string IdCard { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Debe introducir 8 dígitos")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email no válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Mascota")]
        public string PetName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Tipo")]
        public string PetType { get; set; }

        [Display(Name = "Edad")]
        public int PetAge { get; set; }

        [MinLength(2)]
        [Display(Name = "Raza")]
        public string Race { get; set; }
    }
}
