using System.Collections.Generic;

namespace WiredBrainCoffee.StorageApp.Repositories
{
    public interface IReadRepository<out T> 
    {
        IEnumerable<T> GetAll();
        T GetbyId(int id);
    }
}