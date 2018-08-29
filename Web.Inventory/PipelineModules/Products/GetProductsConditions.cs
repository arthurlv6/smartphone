
using DataModel;
using Microsoft.EntityFrameworkCore;
using PipelineFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Web.API.Events.Products;
using Web.API.Helpers;
using Web.API.PipelineContexts.Products;

namespace Web.API.Modules.Products
{
    public class GetProductsConditions : PipelineModule<GetProductsEvent>,IGetProducts
    {
        public override void Initialize(GetProductsEvent events)
        {
            events.Conditions += (ctx) =>
            {
                try
                {
                    Expression<Func<Product, bool>> searchName;

                    if (string.IsNullOrEmpty(ctx.Input.Name))
                    {
                        searchName = d => true;
                    }
                    else
                    {
                        searchName = d => d.Name.Contains(ctx.Input.Name);
                    }

                    ctx.Query = ctx.Dbcontext.Product.ApplySort(ctx.Input.Sort)
                        .Where(searchName)
                        .Where(d => d.SettingId == 1 && d.Status != "deleted")
                        
                        .OrderByDescending(d => d.CreatedDate);
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
