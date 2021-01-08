using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Nome { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
