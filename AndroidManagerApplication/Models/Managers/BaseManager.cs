using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using AndroidManagerApplication.Models.Contexts;
using AndroidManagerApplication.Models.Entities;

namespace AndroidManagerApplication.Models.Managers
{
    // Provide access to single DB context and CRUD operations for T entities
    public abstract class BaseManager<T> where T: BaseEntity
    {
        protected AndroidDbContext _dataSource;

        public BaseManager()
        {
            _dataSource = AndroidDbContext.Instance();
        }

        abstract protected DbSet<T> GetDbSet();

        // Get list of entities with update from DataBase
        public List<T> GetList()
        {
            return GetDbSet().ToList();
        }

        // Update entity set's content in data context
        public void ReloadList()
        {
            GetList();
        }

        public void Add(T item)
        {
            GetDbSet().Add(item);
            _dataSource.SaveChanges();
        }

        public void Delete(T item)
        {
            if (GetDbSet().Any(e => e.Id == item.Id))
                GetDbSet().Remove(item);
            _dataSource.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var item = GetById(id);
            Delete(item);
        }

        public T GetById(int id)
        {
            return GetDbSet().FirstOrDefault(e => e.Id == id);
        }
    }
}