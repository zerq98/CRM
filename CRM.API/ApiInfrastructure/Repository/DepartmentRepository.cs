using ApiDomain.Entity;
using ApiDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context) { }
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

                return null;
            }
        }
    }
}
