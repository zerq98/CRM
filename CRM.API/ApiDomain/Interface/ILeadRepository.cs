using ApiDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface ILeadRepository
    {
        Task<Lead> AddLeadAsync(Lead lead);

        Task<List<Lead>> GetAllLeadsAsync(int companyId,DateTime DateFrom,DateTime DateTo);

        Task<Lead> GetLeadAsync(int leadId,int companyId);
        Task<Lead> GetLeadByNameAsync(string name, int companyId);
        Task<Lead> UpdateAsync(Lead lead);
        Task RemoveLeadContactAsync(int contactId);
        Task RemoveActivityAsync(int activityId);
        Task<bool> CheckIfRegonExistsAsync(string regon, int leadId,int companyId);
        Task<bool> CheckIfNIPExistsAsync(string nip, int leadId,int companyId);
        Task<List<int>> GetUserActivitiesCountAsync(string userId);
    }
}
