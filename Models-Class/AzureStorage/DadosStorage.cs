using Azure.Storage.Blobs;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents;
using Models_Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class DadosStorage
    {
        public static string StringBlopService { get; set; }
        public static string ContainerName { get; set; }
        public static string RowKey { get; set; }

        public readonly AuthenticatedUser _user;

        public DadosStorage(AuthenticatedUser user)
        {
            _user = user;
            StringBlopService = "DefaultEndpointsProtocol=https;AccountName=ac2020storage;AccountKey=5fAS2v1hAZnoxilyas06ZvZwd7ehsftjBQkGlhsnW8+qtGiqboSO3UhsMS4+y59mx+DKJhmulzSx4NG2UF78SQ==;EndpointSuffix=core.windows.net";
            ContainerName = "listaCarrinhoComprasG1";
        }
        //CONNECTION PARA A LISTA DE CARRINHO DE COMPRAS
        public static CloudTable Conection()
        {
            string ConncetionString = StringBlopService;
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConncetionString);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable tableListaDeCompras = tableClient.GetTableReference("listaCarrinhoComprasG1");
            tableListaDeCompras.CreateIfNotExistsAsync().GetAwaiter().GetResult(); ;
            return tableListaDeCompras;
        }
        //METODO PARA INSERIR ITENS NA LISTA DE CLIENTE(STORAGE)
        public void InserirDados(Produto produto, AuthenticatedUser user, int qta)
        {
            ListaDeProdCliente produtoAddCart = new ListaDeProdCliente(user.UserName, produto.ProdutoId.ToString()) { ProdutoId = produto.ProdutoId, Nome = produto.Nome, Quantidade = qta, Preco = produto.Preco, Url = produto.Url };
            TableBatchOperation tableOperations = new TableBatchOperation();
            tableOperations.InsertOrMerge(produtoAddCart);
            DadosStorage.Conection().ExecuteBatchAsync(tableOperations);
        }
        //METODO PARA APAGAR UM ITEM DA TABLE DO CLIENTE SE ELIMINAR NO CARRINHO
        public async Task ApagarProdutoDaListaAsync(Produto produto, AuthenticatedUser user)
        {   
            TableOperation tableOperation = TableOperation.Retrieve<ListaDeProdCliente>(user.UserName, produto.ProdutoId.ToString());
            TableResult resultado = await Conection().ExecuteAsync(tableOperation);
            ListaDeProdCliente produtoAEliminar = resultado.Result as ListaDeProdCliente;

            TableBatchOperation tableOperations = new TableBatchOperation();
            tableOperations.Delete(produtoAEliminar);
            await Conection().ExecuteBatchAsync(tableOperations);
        }
        //METODO PARA LISTAR TODOS OS PRODUTOS DA LISTA
        public List<ListaDeProdCliente> ListaDeCliente(AuthenticatedUser user)
        {
            string filtro = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, user.UserName);
            TableQuery<ListaDeProdCliente> query = new TableQuery<ListaDeProdCliente>().Where(filtro);
            TableContinuationToken token = new TableContinuationToken();
            var lista = Conection().ExecuteQuerySegmentedAsync(query, token).GetAwaiter().GetResult();
            List<ListaDeProdCliente> listaDeProds = new List<ListaDeProdCliente>();
            foreach (ListaDeProdCliente item in lista)
            {
                listaDeProds.Add(item);
            }
            return listaDeProds;
        }




        //METODOS PARA FAZER UPLOAD DE FOTOS
        public BlobContainerClient OperacaoDeLigaçãoNova()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(StringBlopService);
            string containername = ContainerName;
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containername);
            return blobContainerClient;
        }

        public BlobContainerClient OperacaoDeLigaçãoExistente()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(StringBlopService);
            string containername = ContainerName;
            BlobContainerClient blobContainerClientExist = blobServiceClient.GetBlobContainerClient(containername);
            return blobContainerClientExist;
        }
    }
}
