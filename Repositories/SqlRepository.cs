using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WiredBrainCoffee.StorageApp.Entities;

namespace WiredBrainCoffee.StorageApp.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _set;

        public SqlRepository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }
        public event EventHandler<T>? ItemAdded;

        public T GetbyId(int id)
        {
            return _set.Find(id);
        }
        public void Remove(T item)
        {
            _set.Remove(item);
        }

        public void Add(T item)
        {
            _set.Add(item);
            ItemAdded?.Invoke(this,item);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _set.OrderBy(item => item.Id).ToList();
        }
    }
}