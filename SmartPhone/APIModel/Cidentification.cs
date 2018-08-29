using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class Cidentification
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Idtype { get; set; }
        public string Idnum { get; set; }
        public string ExpiryDate { get; set; }
        public string Country { get; set; }
        public string Version { get; set; }

        public Customer Customer { get; set; }
    }
}
