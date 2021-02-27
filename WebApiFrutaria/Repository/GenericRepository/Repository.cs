using Microsoft.EntityFrameworkCore;
using Models;
using Models_Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrutaria.DataContext;

namespace WebApiFrutaria.Repository.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly ContextApplication _contextApplication;
        private DbSet<T> EntityModel { get; set; }

        public Repository(ContextApplication contextApplication)
        {
            _contextApplication = contextApplication;
            EntityModel = contextApplication.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                if (item == null) return null;
                _contextApplication.Add(item);
                _contextApplication.SaveChanges();

                return item;
            }
            catch (Exception msg)
            {
                Debug.Print(msg.Message);
                return item;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = EntityModel.FirstOrDefault(model => model.Id == id);
                if (result == null) return false;

                EntityModel.Remove(result);
                _contextApplication.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> FindAll()
        {
            try
            {
                var result = EntityModel.ToList();
                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T FindById(int id)
        {
            try
            {
                var result = EntityModel.FirstOrDefault(item => item.Id == id);
                if (result == null) return null;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Update(T item)
        {
            try
            {
                if (item == null) return null;
                var result = EntityModel.FirstOrDefault(i => i.Id == item.Id);
                if (result == null) return null;
                EntityModel.Update(result);
                _contextApplication.SaveChanges();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Search(string item)
        {
            return item;
        }
    }
}
