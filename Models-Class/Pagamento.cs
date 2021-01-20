using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models_Class
{
    public class Pagamento
    {
        public int Id { get; set; }

        [ForeignKey("CarrihoCompra")]
        public int CarrinhoId { get; set; }
        public CarrinhoCompra CarrihoCompra { get; set; }

        [ForeignKey("Encomenda")]
        public int EncomendaId { get; set; }
        public Encomenda Encomenda { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
