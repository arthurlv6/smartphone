using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            InverseParent = new HashSet<ProductCategory>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? ShowOrder { get; set; }
        public int? SettingId { get; set; }
        public string Status { get; set; }

        public ProductCategory Parent { get; set; }
        public Setting Setting { get; set; }
        public ICollection<ProductCategory> InverseParent { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
