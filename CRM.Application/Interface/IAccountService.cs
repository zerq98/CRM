using CRM.Application.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Interface
{
    public interface IAccountService
    {
        Task<string> LoginAsync(LoginDto model);
    }
}
