﻿using ApiDomain.Entity;
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

        Task<List<Lead>> GetAllLeadsAsync(int companyId);

        Task<Lead> GetLeadAsync(int leadId,int companyId);
        Task<Lead> UpdateAsync(Lead lead);
        Task RemoveLeadContactAsync(int contactId);
        Task RemoveActivityAsync(int activityId);
    }
}