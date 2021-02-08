using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_Class
{
    public class ListaDeProdCliente : TableEntity
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required]
        [DisplayName("Produto")]
        public string Nome { get; set; }
        [Required]
        [DisplayName("Preço")]
        public double Preco { get; set; }
        [Required]
        public string Url { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [Required]
        [DisplayName("Quantidade")]
        public int Quantidade { get; set; }

        public ListaDeProdCliente()
        {
        }

        public ListaDeProdCliente(string deptName, string empName)
        {
            PartitionKey = deptName;
            RowKey = empName;
        }
    }
}
