using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Contract = new HashSet<Contract>();
            Purchase = new HashSet<Purchase>();
            WarehouseAdjustment = new HashSet<WarehouseAdjustment>();
            WarehouseInitialization = new HashSet<WarehouseInitialization>();
            WarehouseProduct = new HashSet<WarehouseProduct>();
            WarehouseTransferFrom = new HashSet<WarehouseTransfer>();
            WarehouseTransferTo = new HashSet<WarehouseTransfer>();
        }

        public int Id { get; set; }
        public string Sid { get; set; }
        public string Name { get; set; }
        public int SettingId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public Setting Setting { get; set; }
        public ICollection<Contract> Contract { get; set; }
        public ICollection<Purchase> Purchase { get; set; }
        public ICollection<WarehouseAdjustment> WarehouseAdjustment { get; set; }
        public ICollection<WarehouseInitialization> WarehouseInitialization { get; set; }
        public ICollection<WarehouseProduct> WarehouseProduct { get; set; }
        public ICollection<WarehouseTransfer> WarehouseTransferFrom { get; set; }
        public ICollection<WarehouseTransfer> WarehouseTransferTo { get; set; }
    }
}
