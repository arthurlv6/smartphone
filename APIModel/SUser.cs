using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class SUser
    {
        public SUser()
        {
            ContractAgree = new HashSet<Contract>();
            ContractDelivery = new HashSet<Contract>();
            ContractSales = new HashSet<Contract>();
            ContractUser = new HashSet<Contract>();
            Purchase = new HashSet<Purchase>();
            SUserLink = new HashSet<SUserLink>();
            SUserLog = new HashSet<SUserLog>();
            WarehouseAdjustment = new HashSet<WarehouseAdjustment>();
            WarehouseInitialization = new HashSet<WarehouseInitialization>();
            WarehouseTransfer = new HashSet<WarehouseTransfer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Profile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Type { get; set; }
        public int? GroupId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Phone { get; set; }
        public bool? SystemUser { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }
        public string WorkTime { get; set; }
        public int? SettingId { get; set; }
        public string Status { get; set; }

        public Setting Setting { get; set; }
        public ICollection<Contract> ContractAgree { get; set; }
        public ICollection<Contract> ContractDelivery { get; set; }
        public ICollection<Contract> ContractSales { get; set; }
        public ICollection<Contract> ContractUser { get; set; }
        public ICollection<Purchase> Purchase { get; set; }
        public ICollection<SUserLink> SUserLink { get; set; }
        public ICollection<SUserLog> SUserLog { get; set; }
        public ICollection<WarehouseAdjustment> WarehouseAdjustment { get; set; }
        public ICollection<WarehouseInitialization> WarehouseInitialization { get; set; }
        public ICollection<WarehouseTransfer> WarehouseTransfer { get; set; }
    }
}
