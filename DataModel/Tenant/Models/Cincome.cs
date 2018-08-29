using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Cincome
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal? Wages { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string LengthOfService { get; set; }
        public string EmployeeType { get; set; }
        public string PayPeriod { get; set; }
        public decimal? PayAfterTax { get; set; }
        public string WithdrawDay { get; set; }

        public Customer Customer { get; set; }
    }
}
