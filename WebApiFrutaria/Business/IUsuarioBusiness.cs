using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrutaria.Business

{
    public interface IUsuarioBusiness
    {
        Usuario Create(Usuario Usuario);
        List<Usuario> FindAll();
        Usuario FindById(int id);
        Usuario Update(Usuario Usuario);
        bool Delete(int id);
    }
}
