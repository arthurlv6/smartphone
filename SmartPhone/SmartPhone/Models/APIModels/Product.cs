using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SmartPhone.Models.APIModels
{
    public partial class Product 
    {
        public Product()
        {
            ContractProduct = new HashSet<ContractProduct>();
            ProductDescription = new HashSet<ProductDescription>();
            ProductImage = new HashSet<ProductImage>();
            ProductPayPlan = new HashSet<ProductPayPlan>();
            ProductProperty = new HashSet<ProductProperty>();
            PurchaseProduct = new HashSet<PurchaseProduct>();
            WarehouseInitialization = new HashSet<WarehouseInitialization>();
            WarehouseProduct = new HashSet<WarehouseProduct>();
            WarehouseTransferDetail = new HashSet<WarehouseTransferDetail>();
        }
        public ICommand UpdownCommand { get; set; }
        public ICommand CallSupplier { get; set; }
        public ICommand SupplierAddressCommand { get; set; }
        public string UpdownImage { get; set; } = "expand.png";
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UserId { get; set; }
        public string SeriesNumber { get; set; }
        public string BarCode { get; set; }
        public string Profile { get; set; }
        public int? CategoryId { get; set; }
        public bool? ShowOn { get; set; }
        public int? ShowOrder { get; set; }
        public decimal? PerPay { get; set; }
        public int? SettingId { get; set; }
        public string ProductCode { get; set; }
        public decimal? TotalValue { get; set; }
        public string Status { get; set; }
        public int? SupplierId { get; set; }
        public decimal? BasicPrice { get; set; }
        public decimal? Rrp { get; set; }
        public bool ShowDetail { get; set; } = false;
        public string Category { get; set; }
        public Setting Setting { get; set; }
        public Supplier Supplier { get; set; }
        public string Warehouse { get; set; }
        public ICollection<ContractProduct> ContractProduct { get; set; }
        public ICollection<ProductDescription> ProductDescription { get; set; }
        public ICollection<ProductImage> ProductImage { get; set; }
        public ICollection<ProductPayPlan> ProductPayPlan { get; set; }
        public ICollection<ProductProperty> ProductProperty { get; set; }
        public ICollection<PurchaseProduct> PurchaseProduct { get; set; }
        public ICollection<WarehouseInitialization> WarehouseInitialization { get; set; }
        public ICollection<WarehouseProduct> WarehouseProduct { get; set; }
        public ICollection<WarehouseTransferDetail> WarehouseTransferDetail { get; set; }
    }
}
