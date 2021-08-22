using ApiDomain.Entity;
using ApiDomain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            try
            {
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();
                return department;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "DepartmentRepository/CreateDepartmentAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }

        public Task DeleteDepartmentAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Id == departmentId);
        }
    }
}