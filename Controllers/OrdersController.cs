using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        private readonly DemoContext _context;

        public OrdersController(DemoContext context)
        {
            _context = context;
            _log4net = log4net.LogManager.GetLogger(typeof(OrdersController));
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<Orders> GetOrders()
        {
            return _context.Orders.ToList();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public Orders GetOrders(int id)
        {
            var orders = _context.Orders.Find(id);

            return orders;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutOrders(int id, Orders orders)
        {
            Orders o = _context.Orders.Find(id);
            o.Email = orders.Email;
            o.FoodId = orders.FoodId;

            //_context.Entry(orders).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                return Ok("Order Updation Successfull");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

 
        }

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(Orders orders)
        {
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = orders.OrderId }, orders);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Orders>> DeleteOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return orders;
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
