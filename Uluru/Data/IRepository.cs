using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Data
{
    public interface IRepository <T>
    {
        bool Exists(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> Remove(int id);
        Task Update(int id, T newValue);
        Task Add(T toAdd);
    }

}
