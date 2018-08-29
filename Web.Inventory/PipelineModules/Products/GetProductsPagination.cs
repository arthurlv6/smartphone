using PipelineFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Events.Products;
using Web.API.Modules.Products;

namespace Web.API.PipelineModules.Products
{
    public class GetProductsPagination : PipelineModule<GetProductsEvent>, IGetProducts
    {
        public override void Initialize(GetProductsEvent events)
        {
            events.Pagination += (ctx) =>
            {
                try
                {
                    // ensure the page size isn't larger than the maximum.
                    if (ctx.Input.PageSize > 15)
                    {
                        ctx.Input.PageSize = 15;
                    }
                    // calculate data for metadata
                    var totalCount = ctx.Query.Count();
                    var totalPages = (int)Math.Ceiling((double)totalCount / ctx.Input.PageSize);

                    var paginationHeader = new
                    {
                        currentPage = ctx.Input.Page,
                        pageSize = ctx.Input.PageSize,
                        totalCount = totalCount,
                        totalPages = totalPages,
                        previousPageLink = "prevLink",
                        nextPageLink = "nextLink"
                    };
                    ctx.Pagination= Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader);
                }
                catch (Exception)
                {
                    ctx.Cancel = true;
                    throw;
                }
            };
        }
    }
}
