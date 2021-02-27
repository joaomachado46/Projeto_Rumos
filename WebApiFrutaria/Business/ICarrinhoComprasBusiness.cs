using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrutaria.Business

{
    public interface ICarrinhoComprasBusiness
    {
        CarrinhoCompra Create(CarrinhoCompra CarrinhoCompra);
        List<CarrinhoCompra> FindAll();
        CarrinhoCompra FindById(int id);
        CarrinhoCompra Update(CarrinhoCompra CarrinhoCompra);
        bool Delete(int id);
    }
}
