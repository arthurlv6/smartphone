using APIModel.Mapping;
using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class ProductPayPlan : IMapFrom<DataModel.ProductPayPlan>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string PayPlan { get; set; }
        public string Term { get; set; }
        public int Delivery { get; set; }
        public decimal Price { get; set; }

        public Product Product { get; set; }
    }
}
