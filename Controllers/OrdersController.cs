using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;
using RestaurantManagement.Repository;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        IOrdersRep db;

        public OrdersController(IOrdersRep _db)
        {
            db = _db;
            _log4net = log4net.LogManager.GetLogger(typeof(OrdersController));
        }

        // GET: api/Orders
        [HttpGet]
        public IActionResult GetOrders()
        {
            _log4net.Info("OrderController GET ALL ACTION METHODS are called!");
            try
            {
                var det = db.GetDetails();
                if (det == null)
                    return NotFound();
                return Ok(det);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            _log4net.Info("OrderController GET BY ACTION METHOD is called!");
            Orders data = new Orders();
            try
            {
                data = db.GetDetail(id);
                if (data == null)
                {
                    return BadRequest(data);
                }
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest(data);
            }
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /*[HttpPut("{id}")]
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
        */

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult Post([FromBody] Orders order)
        {
            _log4net.Info("OrderController POST ACTION METHOD is called!");
            if (ModelState.IsValid)
            {
                try
                {
                    var res = db.AddDetail(order);
                    if (res != 0)
                        return Ok(res);

                    return NotFound();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }


        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _log4net.Info("OrderController DELETE ACTION METHOD is called!");
            try
            {
                var result = db.Delete(id);
                if (result == 0)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest(id);
            }
        }


        
    }
}
