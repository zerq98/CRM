using System.Collections.Generic;

namespace CRM.Desktop.Data.Models
{
    public class CompanyStatisticsDto
    {
        public List<int> Opportunities { get; set; }
        public List<int> Activities { get; set; }
        public double ThisMonthMarkup { get; set; }
        public double ThisYearMarkup { get; set; }
        public double ThisMonthGross { get; set; }
        public double ThisYearGross { get; set; }
        public double ThisMonthNet { get; set; }
        public double ThisYearNet { get; set; }
    }
}