using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ApiDomain.Interface
{
    public interface IBaseRepository
    {
        IDbContextTransaction GetTransaction();
    }
}