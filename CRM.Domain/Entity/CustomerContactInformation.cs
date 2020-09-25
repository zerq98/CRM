using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Domain.Entity
{
    public class CustomerContactInformation
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public Customer Company { get; set; }

        public List<CustomerContact> CompanyContacts { get; set; }
    }
}
