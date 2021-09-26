namespace ApiApplication.DTO
{
    public class SellOpportunityPositionForListDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double NetValue { get; set; }
        public double Markup { get; set; }
        public double GrossValue { get; set; }
        public double VatValue { get; set; }
        public string Product { get; set; }
    }
}