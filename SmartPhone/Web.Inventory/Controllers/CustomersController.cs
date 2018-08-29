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
    [Route("api/Customers")]
    public class CustomersController : BaseController<CustomersController>
    {
        public CustomersController(InventoryContext context,
            IMapper mapper,
            IDataShapeFactory dataShapeFactory,
            ILogger<CustomersController> logger)
            : base(context, mapper, dataShapeFactory, logger)
        {
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult GetCustomer(string name = null)
        {
            Expression<Func<Customer, bool>> searchName;
            if (string.IsNullOrEmpty(name))
            {
                searchName = d => true;
            }
            else
            {
                searchName = d => d.FirstName.Contains(name);
            }
            var entities = _context.Customer.Where(searchName).OrderByDescending(d => d.CreatedDate);

            var data = _mapper.Map<IEnumerable<APIModel.Customer>>(entities);
            return Ok(data);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}