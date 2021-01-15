using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models_Class.Area
{
    class Admin
    {
        public int Id { get; set; }
        [Required]
        public int Nome { get; set; }
        [Required]
        public int Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string CargoFuncionario { get; }
        [Required]
        public int NumeroFuncionaro { get; set; }
    }
}
