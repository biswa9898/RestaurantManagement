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
    public class UsersController : ControllerBase
    { 
    
        readonly log4net.ILog _log4net;

        private readonly DemoContext _context;

        public UsersController(DemoContext context)
        {
            _context = context;
            _log4net = log4net.LogManager.GetLogger(typeof(UsersController));
    }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<Users> GetUsers()
        {
            return _context.Users.ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public Users GetUsers(string id)
        {
            var users =  _context.Users.Find(id);

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutUsers(string id, [FromBody]Users users)
        {
            // _context.Entry(users).State = EntityState.Modified;
            Users u = _context.Users.Find(id);
            u.FirstName = users.FirstName;
            u.LastName = users.LastName;
            u.Password = users.Password;
            _context.Users.Update(u);

            try
            {
                _context.SaveChanges();
                return Ok("Updation Sucessfull");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsersExists(users.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsers", new { id = users.Email }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(string id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.Email == id);
        }
    }
}
