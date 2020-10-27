using System.Collections.Generic;

namespace CRM.Domain.Entity
{
    public class CustomerContactInformation : Base
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<CustomerContact> CustomerContacts { get; set; }
    }
}