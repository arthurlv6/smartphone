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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Web.API.Infrastructure;
using Web.API.PipelineContexts.Products;
using System.Reflection;
using Web.API.Modules.Products;
using Web.API.Events.Products;
using PipelineFramework;

namespace Web.API.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : BaseController<ProductsController>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductsController(
            InventoryContext context, 
            IMapper mapper, 
            IDataShapeFactory dataShapeFactory,
            ILogger<ProductsController> logger,
            IHostingEnvironment hostingEnvironment) 
            :base(context,mapper,dataShapeFactory,logger)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProduct(string sort = "name", string name = null, string fields = null, int page = 1, int pageSize = maxPageSize)
        {
            var pipelineContext = new GetProductsPipelineContext()
            {
                Input = new RequestInput() { Name = name, Fields = fields, Page = page, PageSize = pageSize },
                Dbcontext = _context,
                Mapper = _mapper,
                DataShapeFactory = _dataShapeFactory
            };
            var modules = from type in Assembly.GetExecutingAssembly().GetTypes()
                          where typeof(IGetProducts).IsAssignableFrom(type) && !type.IsInterface
                          select type;

            var backbone = new Backbone<GetProductsEvent>(modules);
            try
            {
                backbone.Execute(pipelineContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

            HttpContext.Response.Headers.Add("X-Pagination", pipelineContext.Pagination);
            return Ok(pipelineContext.Data);
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

            var product = await _context.Product
                .Include(d => d.ProductProperty)
                .Include(d => d.Category)
                .Include(d => d.Supplier)
                .SingleOrDefaultAsync(m => m.BarCode == barcode);

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
        private string HandleImage()
        {
            var fileName = "";
            var httpRequest = HttpContext.Request;
            if (httpRequest.Form.Files.Count > 0)
            {
                foreach (var file in httpRequest.Form.Files)
                {
                    fileName = file.FileName.Split("\\").LastOrDefault().Split('/').LastOrDefault();
                    var filePath = _hostingEnvironment.WebRootPath + "\\images\\products\\";
                   
                    using (FileStream fs = System.IO.File.Create(filePath + fileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }
            return fileName;
        }
        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] APIModel.Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var p = _mapper.Map<Product>(product);
                p.CreatedDate= DateTime.Now;
                p.SettingId = 1;
                p.CategoryId = 20;
                _context.Product.Add(p);
                await _context.SaveChangesAsync();
                _logger.LogInformation("creating a new product.");
                return CreatedAtAction("GetProduct", new { id = p.Id }, p);
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