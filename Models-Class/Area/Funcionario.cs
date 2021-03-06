using Models_Class;
using Models_Class.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Funcionario : BaseEntity
    {
        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Número Empregado")]
        public int NumeroDeTrabalhador { get; set; }

        [Required]
        public string Cargo { get; set; }
    }
}
