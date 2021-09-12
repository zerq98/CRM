using ApiDomain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface IClaimRepository
    {
        Task<List<string>> GetApplicationClaimsAsync();
    }
}