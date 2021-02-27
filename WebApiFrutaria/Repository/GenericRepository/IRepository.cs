using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrutaria.Repository.GenericRepository
{
    public interface IRepository<T>
    {
        T Create(T item);
        T FindById(int id);
        List<T> FindAll();
        T Update(T item);
        bool Delete(int id);
        string Search(string item);
    }
}
