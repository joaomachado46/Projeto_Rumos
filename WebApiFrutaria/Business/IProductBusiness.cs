using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrutaria.Business
{
    public interface IProductBusiness
    {
        Produto Create(Produto produto);
        Produto FindById(int id);
        List<Produto> FindAll();
        Produto Update(Produto produto);
        bool Delete(int id);
        string SaveImageAzureBlob(string files);
    }
}
