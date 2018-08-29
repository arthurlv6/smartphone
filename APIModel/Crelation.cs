using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class Crelation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string HouseNum { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Customer Customer { get; set; }
    }
}
