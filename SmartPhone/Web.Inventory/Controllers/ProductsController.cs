using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModel;
using AutoMapper;
using Web.API.Infrastructure.Contracts;
using Web.API.Helpers;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Web.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : BaseController<ProductsController>
    {
        public ProductsController(
            InventoryContext context, 
            IMapper mapper, 
            IDataShapeFactory dataShapeFactory,
            ILogger<ProductsController> logger) 
            :base(context,mapper,dataShapeFactory,logger)
        {
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProduct(string sort="name",string name=null,string fields=null, int page = 1, int pageSize = maxPageSize)
        {
            Expression<Func<Product, bool>> searchName;
            if (string.IsNullOrEmpty(name))
            {
                searchName = d =>true;
            }
            else
            {
                searchName = d => d.Name.Contains(name);
            }
            var data = _mapper.Map<IEnumerable<APIModel.Product>>(_context.Product.ApplySort(sort)
                .Where(searchName)
                .Where(d=>d.SettingId==1&&d.Status!="deleted")
                .Include(d=>d.Category).Include(d=>d.Supplier)
                .OrderByDescending(d=>d.CreatedDate));

            // ensure the page size isn't larger than the maximum.
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            // calculate data for metadata
            var totalCount = data.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var paginationHeader = new
            {
                currentPage = page,
                pageSize = pageSize,
                totalCount = totalCount,
                totalPages = totalPages,
                previousPageLink = "prevLink",
                nextPageLink = "nextLink"
            };

            HttpContext.Response.Headers.Add("X-Pagination",
               Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            //shape data
            List<string> lstOfFields = new List<string>();

            if (fields != null)
            {
                lstOfFields = fields.ToLower().Split(',').ToList();
            }

            // return result
            return Ok(
            data.Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList()
                .Select(d => _dataShapeFactory.CreateDataShapedObject(d, lstOfFields)));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id,string fields=null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product
                .Include(d=>d.ProductProperty)
                .Include(d=>d.Category)
                .Include(d => d.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<APIModel.Product>(product));
        }
        [HttpGet("barcode/{barcode}")]
        [Route("")]
        public async Task<IActionResult> GetProduct([FromRoute] string barcode, string fields = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.BarCode == barcode);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<APIModel.Product>(product));
        }
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] APIModel.Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                var p = _mapper.Map<Product>(product);
                p.CreatedDate = DateTime.Now;
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Product.Add(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation("creating a new product.");
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception  ex)
            {
                _logger.LogError($"Threw exception while save product: {ex}");
            }
            return BadRequest();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}