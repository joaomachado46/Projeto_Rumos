using Models_Class;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Pagamento : BaseEntity
    {
        [ForeignKey("CarrihoCompra")]
        public int? CarrinhoId { get; set; }
        public CarrinhoCompra CarrihoCompras { get; set; }

        [ForeignKey("Encomenda")]
        public int? EncomendaId { get; set; }
        public Encomenda Encomendas { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioId { get; set; }
        public Usuario Usuarios { get; set; }

    }
}
