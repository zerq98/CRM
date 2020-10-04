namespace CRM.Application.Dto
{
    public class CustomerEditDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DealSize { get; set; }

        public CustomerAddressDto customerAddress { get; set; }

        public string Description { get; set; }
    }
}