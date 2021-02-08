using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models_Class
{
    public class Contacto
    {
        [Key]
        public int ContactoId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name ="Contacto telefónico")]
        public int ContactoTelefonico { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Mensagem { get; set; }
    }
}
