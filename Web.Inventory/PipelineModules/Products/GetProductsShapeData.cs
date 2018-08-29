using Microsoft.EntityFrameworkCore;
using PipelineFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Events.Products;
using Web.API.Modules.Products;

namespace Web.API.PipelineModules.Products
{
    public class GetProductsShapeData : PipelineModule<GetProductsEvent>, IGetProducts
    {
        public override void Initialize(GetProductsEvent events)
        {
            events.ShapeData += (ctx) =>
            {
                try
                {
                    //shape data
                    List<string> lstOfFields = new List<string>();

                    if (ctx.Input.Fields != null)
                    {
                        lstOfFields = ctx.Input.Fields.ToLower().Split(',').ToList();
                    }

                    // return result

                    ctx.Data=ctx.Mapper.Map<IEnumerable<APIModel.Product>>(ctx.Query.Include(d => d.Category).Include(d => d.Supplier).Skip(ctx.Input.PageSize * (ctx.Input.Page - 1))
                        .Take(ctx.Input.PageSize)
                        .ToList()
                        .Select(d => ctx.DataShapeFactory.CreateDataShapedObject(d, lstOfFields)));
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
