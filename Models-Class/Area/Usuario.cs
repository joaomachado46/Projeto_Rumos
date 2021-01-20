using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string Username { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        public string Morada { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        public int CartaoIdentificacao { get; set; }
        [Required]
        public string Contacto { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public ICollection<Encomenda> Encomendas{ get; set; }
    }
}
