using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Encomenda
    {
        public enum EstadoEncomenda { Finalizada, Pendente, Entregue }

        [Key]
        public int EncomendaId { get; set; }
        public string Nome { get; set; }

        [ForeignKey("Usuario")]
        public int? IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<EncomendaProduto> EncomendaProdutos { get; set; }

        [ForeignKey("CarrinhoCompra")]
        public int? IdCarrinhoCompra { get; set; }
        public CarrinhoCompra CarrinhoCompra { get; set; }

        public EstadoEncomenda Estado { get; set; }
    }
}
