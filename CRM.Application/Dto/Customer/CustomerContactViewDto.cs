using CRM.Application.Mapper;
using CRM.Domain.Entity;

namespace CRM.Application.Dto
{
    public class CustomerContactViewDto
    {
        public int Id { get; set; }

        public string ContactDetail { get; set; }

        public string ContactType { get; set; }
    }
}