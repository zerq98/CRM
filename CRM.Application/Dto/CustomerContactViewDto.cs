using CRM.Application.Mapper;
using CRM.Domain.Entity;

namespace CRM.Application.Dto
{
    public class CustomerContactViewDto : IMapFrom<CustomerContact>
    {
        public int Id { get; set; }

        public string ContactDetail { get; set; }

        public string ContactType { get; set; }

        private void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CustomerContact, CustomerContactViewDto>()
                .ForMember(s => s.ContactType, opt => opt.MapFrom(s => s.ContactType.Name));
        }
    }
}