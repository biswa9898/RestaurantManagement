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
    public class FoodController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        private readonly DemoContext _context;

        public FoodController(DemoContext context)
        {
            _context = context;
            _log4net = log4net.LogManager.GetLogger(typeof(FoodController));
        }

        // GET: api/Food
        [HttpGet]
        public IEnumerable<Food> GetFoods()
        {
            return _context.Foods.ToList();
        }

        // GET: api/Food/5
        [HttpGet("{id}")]
        public Food GetFood(int id)
        {
            var food = _context.Foods.Find(id);
            
            return food;
        }

        // PUT: api/Food/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
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

        // POST: api/Food
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFood", new { id = food.Id }, food);
        }

        // DELETE: api/Food/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Food>> DeleteFood(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            return food;
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
    }
}
