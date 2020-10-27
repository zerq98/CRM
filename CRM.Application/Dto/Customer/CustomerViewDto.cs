using CRM.Application.Mapper;
using CRM.Domain.Entity;
using System.Collections.Generic;

namespace CRM.Application.Dto
{
    public class CustomerViewDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NIP { get; set; }

        public string REGON { get; set; }

        public string KRSNumber { get; set; }

        public int DealSize { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }

        public CustomerAddressViewDto AddressDetails { get; set; }

        public List<CustomerContactViewDto> ContactInformation { get; set; }

        public string Description { get; set; }
    }
}