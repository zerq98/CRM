using CRM.Application.Mapper;
using CRM.Domain.Entity;

namespace CRM.Application.Dto
{
    public class CustomerAddressViewDto
    {
        public int Id { get; set; }
        public string City { get; set; }

        public string Street { get; set; }

        public int BuildingNumber { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }
    }
}