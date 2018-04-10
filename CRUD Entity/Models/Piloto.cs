using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Entity.Models
{
    public class Piloto
    {
        [Key]
        public int IdPiloto { get; set; }
        public int RG { get; set; }
        public int NumeroLicenca { get; set; }
        public long CPFCNPJ { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public ICollection<Aviao> Avioes { get; set; }
    }
}