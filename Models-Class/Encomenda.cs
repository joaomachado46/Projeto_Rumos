using Models_Class;
using Models_Class.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Encomenda : BaseEntity
    {
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<EncomendaProduto> EncomendaProdutos { get; set; }

        public IEnumerable<CarrinhoCompra> CarrinhoCompras { get; set; }

        public EnumEncomenda Estado { get; set; }
    }
}
