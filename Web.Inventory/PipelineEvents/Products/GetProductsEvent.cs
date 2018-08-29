using PipelineFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.PipelineContexts.Products;

namespace Web.API.Events.Products
{
    public class GetProductsEvent: PipelineEvent
    {
        [PipelineEvent(Order = 0, TransactionScopeOption = TransactionScopeOption.Suppress)]
        public Action<GetProductsPipelineContext> Conditions { get; set; }

        [PipelineEvent(Order = 1, TransactionScopeOption = TransactionScopeOption.Suppress)]
        public Action<GetProductsPipelineContext> Pagination { get; set; }
        [PipelineEvent(Order = 2, TransactionScopeOption = TransactionScopeOption.Suppress)]
        public Action<GetProductsPipelineContext> ShapeData { get; set; }
    }
}
