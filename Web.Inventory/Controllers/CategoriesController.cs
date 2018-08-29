using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.API.Infrastructure.Contracts;

namespace Web.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    public class CategoriesController : BaseController<CategoriesController>
    {
        public CategoriesController(
            InventoryContext context,
            IMapper mapper,
            IDataShapeFactory dataShapeFactory,
            ILogger<CategoriesController> logger)
            : base(context, mapper, dataShapeFactory, logger)
        { }
        [HttpGet]
        public IActionResult GetCustomer(string name = null)
        {
            var entities = _context.ProductCategory.OrderByDescending(d => d.Name);

            var data = _mapper.Map<IEnumerable<APIModel.ProductCategory>>(entities);
            return Ok(data);
        }
    }
}