using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class CarrinhoCompra
    {
        [Key]
        public int CarrinhoId { get; set; }
        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }

        //[ForeignKey("Usuario")]
        //public int IdUsuario { get; set; }
        //public Usuario Usuario { get; set; }


        //public string FormaPagamento { get; set; }

        //public ICollection<Encomenda> Encomendas { get; set; }
    }
}
