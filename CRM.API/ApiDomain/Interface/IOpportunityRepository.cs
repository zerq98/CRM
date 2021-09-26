using ApiDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface IOpportunityRepository
    {
        Task<SellOpportunityHeader> AddOpportunityAsync(SellOpportunityHeader opportunity);

        Task<List<SellOpportunityHeader>> GetAllOpportunitiesAsync(int companyId, List<string> filters, DateTime DateFrom, DateTime DateTo);
        Task<List<SellOpportunityHeader>> GetAllOrdersAsync(int companyId, List<string> filters, DateTime DateFrom, DateTime DateTo);
        Task<SellOpportunityHeader> GetOpportunityAsync(int headerId, int companyId);
        Task<SellOpportunityHeader> UpdateAsync(SellOpportunityHeader opportunityHeader);
        Task RemoveOpportunityPositionAsync(int positionId);
        Task<List<int>> GetUserOpportunitiesCountAsync(string userId);
    }
}
