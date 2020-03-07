using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liments.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(string str);
        IEnumerable<T> Find(Func<T, bool> predicate);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task UpdateFieldAsync(T item, string field);
        Task DeleteAsync(string id);
    }
}
