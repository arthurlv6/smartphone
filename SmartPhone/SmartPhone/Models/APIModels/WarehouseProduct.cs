using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class WarehouseProduct
    {
        public WarehouseProduct()
        {
            WarehousePrductRecord = new HashSet<WarehousePrductRecord>();
        }

        public long Id { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal? Low { get; set; }
        public decimal? High { get; set; }

        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
        public ICollection<WarehousePrductRecord> WarehousePrductRecord { get; set; }
    }
}
