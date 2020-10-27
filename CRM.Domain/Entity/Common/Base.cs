using System;

namespace CRM.Domain.Entity
{
    public class Base
    {
        public int Id { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}