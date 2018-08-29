using APIModel.Mapping;
using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class ProductDescription : IMapFrom<DataModel.ProductDescription>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }

        public Product Product { get; set; }
    }
}
