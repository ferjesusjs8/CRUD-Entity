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
        public int PilotoRefId { get; set; }
        public Piloto Pilotos { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Ano { get; set; }
    }
}