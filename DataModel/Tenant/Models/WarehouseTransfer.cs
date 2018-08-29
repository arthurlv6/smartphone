using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class WarehouseTransfer
    {
        public WarehouseTransfer()
        {
            WarehouseTransferDetail = new HashSet<WarehouseTransferDetail>();
        }

        public long Id { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public DateTime OperationDate { get; set; }
        public int SUserId { get; set; }

        public Warehouse From { get; set; }
        public SUser SUser { get; set; }
        public Warehouse To { get; set; }
        public ICollection<WarehouseTransferDetail> WarehouseTransferDetail { get; set; }
    }
}
