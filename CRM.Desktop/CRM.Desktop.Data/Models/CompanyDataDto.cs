namespace CRM.Desktop.Data.Models
{
    public class CompanyDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public AddressDto Address { get; set; }
    }
}