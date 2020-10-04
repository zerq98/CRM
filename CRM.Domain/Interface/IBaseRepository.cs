using System.Linq;
using System.Threading.Tasks;

namespace CRM.Domain.Interface
{
    public interface IBaseRepository<T>
    {
        Task<int> AddAsync(T entity);

        IQueryable<T> GetAll();

        Task SaveAsync();

        Task<T> GetById(int id);
    }
}