using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUD_Entity.Models
{
    public class Piloto
    {
        [Key]
        [Required]
        public int IdPiloto { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Este campo deve conter no mínimo 3 caracteres e no máximo 20.")]
        [Display(Name = "RG")]
        public string RG { get; set; }

        [Required]
        [Display(Name = "Número da Licença")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Este campo deve conter no mínimo 6 caracteres e no máximo 16.")]
        public string NumeroLicenca { get; set; }

        [Required]
        [Display(Name ="CPF / CNPJ")]
        [StringLength(16, MinimumLength = 11, ErrorMessage = "Este campo deve conter no mínimo 11 caracteres e no máximo 16.")]
        public string CPFCNPJ { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Este campo deve conter no mínimo 3 caracteres e no máximo 70.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name = "Ativo?")]
        public bool Ativo { get; set; }

        public ICollection<Aviao> Avioes { get; set; }
    }
}