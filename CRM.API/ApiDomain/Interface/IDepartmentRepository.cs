using ApiDomain.Entity;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateDepartmentAsync(Department department);

        Task DeleteDepartmentAsync(int departmentId);
        Task<Department> GetDepartmentByIdAsync(int departmentId);
    }
}