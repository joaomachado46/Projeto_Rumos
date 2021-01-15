using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Morada { get; set; }
        public DateTime DataNascimento { get; set; }
        public int CartaoIdentificacao { get; set; }
        public int Contacto { get; set; }
        public string Email { get; set; }

        public ICollection<Encomenda> Encomendas{ get; set; }
    }
}
