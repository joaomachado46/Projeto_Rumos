using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required]
        [DisplayName("Produto")]
        public string Nome { get; set; }
        [Required]
        [DisplayName("Preço")]
        public float Preco { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [Required]
        [DisplayName("Picture")]
        public string PhotoFileName { get; set; }
        public string ImageMimeType { get; set; }
        [Required]
        [DisplayName("Stock")]
        public int Stock { get; set; }

        [ForeignKey("CarrinhoCompra")]
        public int CarrinhoId { get; set; }
        public CarrinhoCompra Carrinho { get; set; }

        [ForeignKey("Categoria")]
        public int? IdCategoria { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<EncomendaProduto> EncomendaProdutos { get; set; }
    }
}
