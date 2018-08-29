using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class CbankDetail
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string Suffix { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string NameOnAcc { get; set; }
        public string Note { get; set; }

        public Customer Customer { get; set; }
    }
}
