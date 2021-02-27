using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrutaria.Repository.GenericRepository;

namespace WebApiFrutaria.Business.Implementation
{
    public class UsuarioBusinessImplementation : IUsuarioBusiness
    {
        private readonly IRepository<Usuario> _repository;

        public UsuarioBusinessImplementation(IRepository<Usuario> repository)
        {
            _repository = repository;
        }

        public Usuario Create(Usuario Usuario)
        {
            try
            {
                if (Usuario == null) return null;
                var result = _repository.Create(Usuario);
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

        public List<Usuario> FindAll()
        {
            var Result = _repository.FindAll();
            return Result.ToList();
        }

        public Usuario FindById(int id)
        {
            var Result = _repository.FindById(id);
            return Result;
        }

        public Usuario Update(Usuario Usuario)
        {
            if (Usuario == null) return null;
            var result = _repository.Update(Usuario);
            if (result == null) return null;
            return result;
        }
    }
}
