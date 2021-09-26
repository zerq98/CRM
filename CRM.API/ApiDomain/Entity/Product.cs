using System;

namespace ApiDomain.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double VatRate { get; set; }
        public double UnitValue { get; set; }
        public double MarkupRate { get; set; }
        public string UnitOfMeasurement { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}