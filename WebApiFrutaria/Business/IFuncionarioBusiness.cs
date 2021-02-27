using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrutaria.Business

{
    public interface IFuncionarioBusiness
    {
        Funcionario Create(Funcionario funcionario);
        List<Funcionario> FindAll();
        Funcionario FindById(int id);
        Funcionario Update(Funcionario funcionario);
        bool Delete(int id);
    }
}
