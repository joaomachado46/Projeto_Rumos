using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrutaria.Repository.GenericRepository;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using Newtonsoft.Json;

namespace WebApiFrutaria.Business.Implementation
{
    public class ProductBusinessImplementation : IProductBusiness
    {
        private readonly IRepository<Produto> Repository;
        private readonly DadosStorage DadosStorage;

        public ProductBusinessImplementation(IRepository<Produto> repository, DadosStorage dadosStorage)
        {
            Repository = repository;
            DadosStorage = dadosStorage;
        }

        public Produto Create(Produto produto)
        {
            try
            {
                if (produto == null) return null;
                var result = Repository.Create(produto);
                if (result == null) return null;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = Repository.Delete(id);
                if (result == true) { return true; }
                else { return false; }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<Produto> FindAll()
        {
            try
            {
                var result = Repository.FindAll();
                if (result == null) return null;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Produto FindById(int id)
        {
            try
            {
                var result = Repository.FindById(id);
                if (result == null) return null;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string SaveImageAzureBlob(string files)
        {
            try
            {
                var st = JsonConvert.DeserializeObject<IFormFile>(files);
                byte[] bytes = Convert.FromBase64String(files);
                BlobContainerClient blop = DadosStorage.OperacaoDeLigaçãoExistente();
                string url = null;

                var stream = new MemoryStream(bytes);
                IFormFile file = new FormFile(stream, 0, bytes.Length, "filename", "file.jpg");
                var localFileName = Path.GetFileName(file.FileName);
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);

                BlobClient blobClient = blop.GetBlobClient(localFileName);
                ms.Position = 0;
                blobClient.Upload(ms, true);
                url = blobClient.Uri.OriginalString;

                return url;

            }
            catch (Exception msg)
            {
                throw new Exception(msg.Message);
            }
        }

        public Produto Update(Produto produto)
        {
            try
            {
                if (produto == null) return null;
                var result = Repository.Update(produto);
                if (result == null) return null;
                return result;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
