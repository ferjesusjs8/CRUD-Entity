using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUD_Entity.Models
{
    public class Aviao
    {
        [Key]
        [Required]
        public int IdAviao { get; set; }
        [Display(Name = "Piloto")]
        [ForeignKey("Pilotos")]
        public int PilotoRefId { get; set; }
        [Display(Name = "Piloto")]
        public Piloto Pilotos { get; set; }
        [Required]
        [Display(Name = "Modelo")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Este campo deve conter no mínimo 3 caracteres e no máximo 30.")]
        public string Modelo { get; set; }
        [Required]
        [Display(Name = "Marca")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Este campo deve conter no mínimo 3 caracteres e no máximo 30.")]
        public string Marca { get; set; }

    }
}