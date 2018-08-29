using APIModel.Mapping;
using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class ContractProduct: IMapFrom<DataModel.ContractProduct>
    {
        public int Id { get; set; }
        public long ContractId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string SeriesNumber { get; set; }
        public decimal? TaxRate { get; set; }
        public string Comment { get; set; }
        public decimal? BasicPrice { get; set; }

    }
}
