using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrutaria.Repository.GenericRepository;

namespace WebApiFrutaria.Business.Implementation
{
    public class CarrinhoComprasBusinessImplementation : ICarrinhoComprasBusiness
    {
        private readonly IRepository<CarrinhoCompra> _repository;

        public CarrinhoComprasBusinessImplementation(IRepository<CarrinhoCompra> repository)
        {
            _repository = repository;
        }

        public CarrinhoCompra Create(CarrinhoCompra CarrinhoCompra)
        {
            try
            {
                if (CarrinhoCompra == null) return null;
                var result = _repository.Create(CarrinhoCompra);
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
            if (id.Equals(null) == true)
            {
                var result = _repository.Delete(id);
                if (result.Equals(true)) return result; 
            }
            return false;
        }

        public List<CarrinhoCompra> FindAll()
        {
            var Result = _repository.FindAll();
            return Result.ToList();
        }

        public CarrinhoCompra FindById(int id)
        {
            var Result = _repository.FindById(id);
            return Result;
        }

        public CarrinhoCompra Update(CarrinhoCompra CarrinhoCompra)
        {
            if (CarrinhoCompra == null) return null;
            var result = _repository.Update(CarrinhoCompra);
            if (result == null) return null;
            return result;
        }
    }
}
