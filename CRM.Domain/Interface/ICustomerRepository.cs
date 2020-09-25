using CRM.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interface
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
    }
}
