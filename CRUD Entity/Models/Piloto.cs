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
        [Required]
        [Display(Name ="RG")]
        public int RG { get; set; }
        [Required]
        [Display(Name = "Numero da Licença")]
        public int NumeroLicenca { get; set; }
        [Required]
        [Display(Name = "CPF / CNPJ")]
        public long CPFCNPJ { get; set; }
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        public ICollection<Aviao> Avioes { get; set; }
    }
}