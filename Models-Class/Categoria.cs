using Models_Class;
using System.Collections.Generic;

namespace Models
{
    public class Categoria : BaseEntity
    {
        public string Nome { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
