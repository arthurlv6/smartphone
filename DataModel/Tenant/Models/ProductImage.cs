using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Profile { get; set; }
        public string Description { get; set; }

        public Product Product { get; set; }
    }
}
