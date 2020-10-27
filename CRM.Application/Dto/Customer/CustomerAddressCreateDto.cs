using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Application.Dto.Customer
{
    public class CustomerAddressCreateDto
    {
        public string City { get; set; }

        public string Street { get; set; }

        public int BuildingNumber { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }
    }
}
