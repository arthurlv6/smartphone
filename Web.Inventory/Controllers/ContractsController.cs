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
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Web.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Contracts")]
    public class ContractsController : BaseController<ContractsController>
    {

        public ContractsController(InventoryContext context,
            IMapper mapper,
            IDataShapeFactory dataShapeFactory,
            ILogger<ContractsController> logger)
            : base(context, mapper, dataShapeFactory, logger)
        {
        }

        // GET: api/Contracts
        [HttpGet]
        public IActionResult GetContract(string name=null, int page = 1, int pageSize = maxPageSize)
        {
            Expression<Func<Contract, bool>> searchName;
            if (string.IsNullOrEmpty(name))
            {
                searchName = d => true;
            }
            else
            {
                searchName = d => d.CustomerName.Contains(name);
            }

            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var data = _context.Contract.Where(searchName);
                
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

            var entities = data
                .Include(d => d.ContractProduct)
                .Include(d => d.Sales).Include(d => d.Channel)
                .OrderByDescending(d => d.CreateDate)
                .Skip(pageSize * (page - 1))
                .Take(pageSize);

            var returnData = _mapper.Map<IEnumerable<APIModel.Contract>>(entities);
            return Ok(returnData);
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContract([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contract = await _context.Contract.SingleOrDefaultAsync(m => m.Id == id);

            if (contract == null)
            {
                return NotFound();
            }

            return Ok(contract);
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract([FromRoute] long id, [FromBody] Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contract.Id)
            {
                return BadRequest();
            }

            _context.Entry(contract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
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

        // POST: api/Contracts
        [HttpPost]
        public async Task<IActionResult> PostContract([FromBody] Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contract.Add(contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContract", new { id = contract.Id }, contract);
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contract = await _context.Contract.SingleOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            _context.Contract.Remove(contract);
            await _context.SaveChangesAsync();

            return Ok(contract);
        }

        private bool ContractExists(long id)
        {
            return _context.Contract.Any(e => e.Id == id);
        }
    }
}