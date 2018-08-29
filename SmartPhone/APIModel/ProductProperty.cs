using APIModel.Mapping;
using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class ProductProperty : IMapFrom<DataModel.ProductProperty>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }

        public Product Product { get; set; }
    }
}
