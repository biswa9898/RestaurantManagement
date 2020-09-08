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
    public class FoodController : ControllerBase
    {
        readonly log4net.ILog _log4net;

        IFoodRep db;


        public FoodController(IFoodRep _db)
        {
            db = _db;
            _log4net = log4net.LogManager.GetLogger(typeof(FoodController));
        }

        // GET: api/Food
        [HttpGet]
        public IActionResult GetFoods()
        {
            _log4net.Info("FoodController GET ALL ACTION METHODS are called!");
            try
            {
                var det = db.GetDetails();
                if (det == null)
                    return NotFound();
                return Ok(det);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Food/5
        [HttpGet("{id}")]
        public IActionResult GetFood(int id)
        {
            _log4net.Info("FoodController GET BY ID ACTION METHOD is called!");
            Food data = new Food();
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

        // PUT: api/Food/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /*[HttpPut("{id}")]
        public IActionResult PutFood(int id, Food food)
        {
            Food f = _context.Foods.Find(id);
            f.Name = food.Name;
            f.Price = food.Price;
            f.Type = food.Type;


           // _context.Entry(food).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                return Ok("Food Item Insertion Successfull");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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

        // POST: api/Food
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult Post([FromBody] Food food)
        {
            _log4net.Info("FoodController POST ACTION METHOD is called!");
            if (ModelState.IsValid)
            {
                try
                {
                    var res = db.AddDetail(food);
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

        // DELETE: api/Food/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _log4net.Info("FoodController DELETE ACTION METHODS is called!");
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
