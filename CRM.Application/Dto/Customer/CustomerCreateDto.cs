using System.Collections.Generic;

namespace CRM.Application.Dto.Customer
{
    public class CustomerCreateDto
    {
        public string Name { get; set; }

        public string NIP { get; set; }

        public string REGON { get; set; }

        public string KRSNumber { get; set; }

        public int DealSize { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }

        public CustomerAddressViewDto AddressDetails { get; set; }

        public List<CustomerContactCreateDto> ContactInformation { get; set; }

        public string Description { get; set; }
    }
}