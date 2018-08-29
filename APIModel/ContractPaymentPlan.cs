using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class ContractPaymentPlan
    {
        public int Id { get; set; }
        public long ContractId { get; set; }
        public bool Done { get; set; }
        public DateTime ExecuteDate { get; set; }
        public decimal Money { get; set; }
        public string Status { get; set; }

        public Contract Contract { get; set; }
    }
}
