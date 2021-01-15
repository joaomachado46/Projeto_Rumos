using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models_Class
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int CarrinhoId { get; set; }
        public CarrinhoCompra CarrihoCompra { get; set; }

        public int EncomendaId { get; set; }
        public Encomenda Encomenda { get; set; }

        public int UsuarioId { get; set; }

    }
}
