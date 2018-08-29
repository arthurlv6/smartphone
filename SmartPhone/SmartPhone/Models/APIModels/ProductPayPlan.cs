
using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class ProductPayPlan 
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
