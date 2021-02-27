using System.ComponentModel.DataAnnotations;

namespace Models_Class
{
    public class Contacto : BaseEntity
    {

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
