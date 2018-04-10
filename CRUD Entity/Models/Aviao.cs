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
        public int IdAviao { get; set; }
        [ForeignKey("Pilotos")]
        [Display(Name = "Nome do Piloto")]
        public int PilotoRefId { get; set; }
        [Required]
        [Display(Name = "Nome do Piloto")]
        public Piloto Pilotos { get; set; }
        [Required]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }
        [Required]
        [Display(Name = "Marca")]
        public string Marca { get; set; }
    }
}