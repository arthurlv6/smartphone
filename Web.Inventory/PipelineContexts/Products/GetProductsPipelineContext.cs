using AutoMapper;
using DataModel;
using PipelineFramework;
using System.Collections.Generic;
using System.Linq;
using Web.API.Infrastructure;
using Web.API.Infrastructure.Contracts;

namespace Web.API.PipelineContexts.Products
{
    public class GetProductsPipelineContext : PipelineContext
    {
        public string Message { get; set; }
        public InventoryContext Dbcontext { get; set; }
        public IQueryable<Product> Query { get; set; }
        public IEnumerable<APIModel.Product> Data { get; set; }
        public string Pagination { get; set; }
        public RequestInput Input { get; set; }
        public IMapper Mapper { get; set; }
        public IDataShapeFactory DataShapeFactory { get; set; }
    }
}
