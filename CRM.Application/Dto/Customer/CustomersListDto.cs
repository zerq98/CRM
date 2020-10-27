using System.Collections.Generic;

namespace CRM.Application.Dto
{
    public class CustomersListDto
    {
        public List<CustomerViewDto> Customers { get; set; }

        public int Count { get; set; }
    }
}