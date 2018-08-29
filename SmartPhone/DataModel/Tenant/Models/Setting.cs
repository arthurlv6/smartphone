using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Setting
    {
        public Setting()
        {
            Contract = new HashSet<Contract>();
            Customer = new HashSet<Customer>();
            Product = new HashSet<Product>();
            ProductCategory = new HashSet<ProductCategory>();
            SUser = new HashSet<SUser>();
            Warehouse = new HashSet<Warehouse>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string TradingName { get; set; }
        public int IndustryId { get; set; }
        public int? OrganisationTypeId { get; set; }
        public string Gst { get; set; }
        public string Website { get; set; }
        public int? TimezoneId { get; set; }
        public string Logo { get; set; }
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
        public string Email { get; set; }
        public DateTime? ValidDate { get; set; }
        public bool? Initialized { get; set; }

        public CommonIndustry Industry { get; set; }
        public CommonOrganisationType OrganisationType { get; set; }
        public ICollection<Contract> Contract { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
        public ICollection<SUser> SUser { get; set; }
        public ICollection<Warehouse> Warehouse { get; set; }
    }
}
