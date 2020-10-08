namespace CRM.Domain.Entity
{
    public class Customer : Base
    {
        public string Name { get; set; }

        public string NIP { get; set; }

        public string REGON { get; set; }

        public string KRSNumber { get; set; }

        public int DealSize { get; set; }

        public int StatusId { get; set; }

        public CustomerStatus CustomerStatus { get; set; }

        public bool IsActive { get; set; }

        public int AddressDetailsId { get; set; }

        public CustomerAddressDetails AddressDetails { get; set; }

        public int ContactInformationId { get; set; }

        public CustomerContactInformation ContactInformation { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}