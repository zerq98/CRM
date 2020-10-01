namespace CRM.Domain.Entity
{
    public class CustomerContact : Base
    {
        public int CompanyContactInformationId { get; set; }

        public CustomerContactInformation CustomerContactInformation { get; set; }

        public string ContactDetail { get; set; }

        public int ContactTypeId { get; set; }

        public ContactType ContactType { get; set; }
    }
}