using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class StatementDetail
    {
        public int Id { get; set; }
        public int? StatementId { get; set; }
        public int? CcreferenceId { get; set; }
        public string Reference { get; set; }
        public decimal? Money { get; set; }
        public DateTime? BankDate { get; set; }
        public string Status { get; set; }

        public Statement Statement { get; set; }
    }
}
