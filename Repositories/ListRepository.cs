using System.Collections.Generic;
using System.Linq;
using WiredBrainCoffee.StorageApp.Entities;

namespace WiredBrainCoffee.StorageApp.Repositories
{
    public class ListRepository<T> : IRepository<T>  where T :  IEntity
    {
        protected readonly List<T> _items = new();

        public T GetbyId(int id)
        {
            return _items.Single(item => item.Id == id);
        }
        public void Remove(T item)
        {
            _items.Remove(item);
        }

        // public T Create(T item)
        // {
        //     return new T();
        // }
        public void Add(T item)
        {
            item.Id = _items.Any() ? _items.Max(item => item.Id) +1 : 1;
            _items.Add(item);
        }

        public void Save()
        {
            //Everything is Saved using List<T>
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }
    }
}