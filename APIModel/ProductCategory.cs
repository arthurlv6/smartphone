using APIModel.Mapping;
using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class ProductCategory : IMapFrom<DataModel.ProductCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
