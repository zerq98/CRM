namespace ApiApplication.DTO
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}