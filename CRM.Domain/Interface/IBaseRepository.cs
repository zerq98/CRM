using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interface
{
    public interface IBaseRepository<T>
    {
        Task<int> AddAsync(T entity);

        Task<List<T>> GetAllAsync();

        Task SaveAsync();

        Task<T> GetById(int id);
    }
}
