using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class EncomendaProduto
    {
        public int Id { get; set; }
        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }


        [ForeignKey("Encomenda")]
        public int EncomendaId { get; set; }
        public Encomenda Encomenda { get; set; }
    }
}
