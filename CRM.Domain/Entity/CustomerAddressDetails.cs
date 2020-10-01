namespace CRM.Domain.Entity
{
    public class CustomerAddressDetails : Base
    {
        public string City { get; set; }

        public string Street { get; set; }

        public int BuildingNumber { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public string PostCode { get; set; }

        public int CompanyId { get; set; }

        public Customer Company { get; set; }
    }
}