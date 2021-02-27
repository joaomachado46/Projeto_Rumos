using Models_Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Produto : BaseEntity
    {  
        [DisplayName("Produto")]
        public string Nome { get; set; }
        
        [DisplayName("Preço")]
        public float Preco { get; set; }
        
        [DataType(DataType.MultilineText)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
       
        [DisplayName("Picture")]
        public string PhotoFileName { get; set; }

        public string ImageMimeType { get; set; }
       
        [DisplayName("Stock")]
        public int Stock { get; set; }

        public string Url { get; set; }

        
        [ForeignKey("Categoria")]
        public int? CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        //[ForeignKey("CarrinhoCompra")]
        //public int CarrinhoId { get; set; }
        //public CarrinhoCompra Carrinho { get; set; }

        //public ICollection<EncomendaProduto> EncomendaProdutos { get; set; }

        public Produto()
        {
        }
        public Produto(string nome, float preco, string descricao, string photoFileName, string imageMimeType, int stock, string url)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            PhotoFileName = photoFileName;
            ImageMimeType = imageMimeType;
            Stock = stock;
            Url = url;
        }
    }
}
