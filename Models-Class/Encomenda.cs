using Models_Class.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Encomenda
    {
        [Key]
        public int EncomendaId { get; set; }

        [ForeignKey("Usuario")]
        public int? IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<EncomendaProduto> EncomendaProdutos { get; set; }

        [ForeignKey("CarrinhoCompra")]
        public int? IdCarrinhoCompra { get; set; }
        public CarrinhoCompra CarrinhoCompra { get; set; }

        public EnumEncomenda Estado { get; set; }
    }
}
