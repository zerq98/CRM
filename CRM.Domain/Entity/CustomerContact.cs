using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Domain.Entity
{
    public class CustomerContact
    {
        public int Id { get; set; }

        public int CompanyContactInformationId { get; set; }

        public CustomerContactInformation CompanyContactInformation { get; set; }

        public int ContactTypeId { get; set; }

        public ContactType ContactType { get; set; }
    }
}
