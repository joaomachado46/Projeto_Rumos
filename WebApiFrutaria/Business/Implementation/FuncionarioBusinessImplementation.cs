using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrutaria.Repository.GenericRepository;

namespace WebApiFrutaria.Business.Implementation
{
    public class FuncionarioBusinessImplementation : IFuncionarioBusiness
    {
        private readonly IRepository<Funcionario> _repository;

        public FuncionarioBusinessImplementation(IRepository<Funcionario> repository)
        {
            _repository = repository;
        }

        public Funcionario Create(Funcionario funcionario)
        {
            try
            {
                if (funcionario == null) return null;
                var result = _repository.Create(funcionario);
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
            if (id.Equals(null)) return false;

            var result = _repository.Delete(id);
            if (result.Equals(null)) return false;
            return true;
        }

        public List<Funcionario> FindAll()
        {
            var Result = _repository.FindAll();
            return Result.ToList();
        }

        public Funcionario FindById(int id)
        {
            var Result = _repository.FindById(id);
            return Result;
        }

        public Funcionario Update(Funcionario funcionario)
        {
            if (funcionario == null) return null;
            var result = _repository.Update(funcionario);
            if (result == null) return null;
            return result;
        }
    }
}
