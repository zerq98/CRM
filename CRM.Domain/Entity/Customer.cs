using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Domain.Entity
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NIP { get; set; }

        public string REGON { get; set; }

        public int AddressDetailsId { get; set; }

        public CustomerAddressDetails AddressDetails { get; set; }

        public int ContactInformationId { get; set; }

        public CustomerContactInformation ContactInformation { get; set; }

        public string Description { get; set; }
    }
}
