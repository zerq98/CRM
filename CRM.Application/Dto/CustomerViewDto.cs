using CRM.Application.Mapper;
using CRM.Domain.Entity;
using System.Collections.Generic;

namespace CRM.Application.Dto
{
    public class CustomerViewDto :IMapFrom<Customer>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NIP { get; set; }

        public string REGON { get; set; }

        public string KRSNumber { get; set; }

        public int DealSize { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }

        public CustomerAddressDto AddressDetails { get; set; }

        public List<CustomerContactViewDto> ContactInformation { get; set; }

        public string Description { get; set; }

        private void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Customer, CustomerViewDto>()
                .ForMember(s => s.ContactInformation, opt => opt.MapFrom(x => x.ContactInformation.CustomerContacts));
            profile.CreateMap<CustomerAddressDetails, CustomerAddressDto>();
            profile.CreateMap<CustomerContact, CustomerContactViewDto>();
        }
    }
}