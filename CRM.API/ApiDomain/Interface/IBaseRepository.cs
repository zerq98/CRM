using Microsoft.EntityFrameworkCore.Storage;

namespace ApiDomain.Interface
{
    public interface IBaseRepository
    {
        IDbContextTransaction GetTransaction();
    }
}