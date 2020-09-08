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
    public class UsersController : ControllerBase
    { 
    
        readonly log4net.ILog _log4net;
        IUsersRep db;


        public UsersController(IUsersRep _db)
        {
            db = _db;
            _log4net = log4net.LogManager.GetLogger(typeof(UsersController));
    }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetUsers()
        {
            _log4net.Info("UsersController GET ALL ACTION METHODS are called!");
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

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            _log4net.Info("UsersController GET BY ID ACTION METHOD is called!");
            Users data = new Users();
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

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Users user)
        {
            _log4net.Info("UsersController PUT ACTION METHOD is called!");
            if (ModelState.IsValid)
            {
                try
                {
                    var result = db.UpdateDetail(id, user);
                    if (result != 1)
                        return BadRequest(result);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult Post([FromBody] Users user)
        {
            _log4net.Info("UsersController POST ACTION METHOD is called!");
            if (ModelState.IsValid)
            {
                try
                {
                    var res = db.AddDetail(user);
                    if (res != null)
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


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _log4net.Info("UsersController DELETE ACTION METHODS is called!");
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
