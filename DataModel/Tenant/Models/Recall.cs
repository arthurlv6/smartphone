using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Recall
    {
        public int Id { get; set; }
        public int? CcreferenceId { get; set; }
        public string Status { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? Spdate { get; set; }
        public DateTime? PpsendDate { get; set; }
        public string ProductCategory { get; set; }
        public int? PaymentId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? ArrearsAmount { get; set; }
        public decimal? TotalArrearsAmount { get; set; }
        public int? AdminId { get; set; }
        public decimal? ArrearsFee { get; set; }
    }
}
