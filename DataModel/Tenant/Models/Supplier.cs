using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Supplier
    {
        public Supplier()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Sid { get; set; }
        public string Name { get; set; }
        public string Gstnumber { get; set; }
        public string Note { get; set; }
        public string TaxRate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string PaymentTerm { get; set; }
        public string PostalName { get; set; }
        public string PostalAddress { get; set; }
        public string PostalSuburb { get; set; }
        public string PostalCity { get; set; }
        public string PostalState { get; set; }
        public string PostalCountry { get; set; }
        public string PostalCode { get; set; }
        public string PhysicalName { get; set; }
        public string PhysicalStreetAddress { get; set; }
        public string PhysicalSuburb { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalState { get; set; }
        public string PhysicalCountry { get; set; }
        public string PhysicalPostalCode { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactOfficePhone { get; set; }
        public string ContactWebsite { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactFaxNumber { get; set; }
        public string ContactMobileNumber { get; set; }
        public string ContactDdinumber { get; set; }
        public string ContactTollFreeNumber { get; set; }
        public int? SettingId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? SUserId { get; set; }
        public string Status { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
