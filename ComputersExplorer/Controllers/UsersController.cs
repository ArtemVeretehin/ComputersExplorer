using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputersExplorer;
using ComputersExplorer.Models;
using ComputersExplorer.CustomAuthenticationSchemes.GUID;
using Microsoft.AspNetCore.Authorization;

namespace ComputersExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ComputersExplorerContext context;
        private readonly IGUIDAuthenticationManager GUIDAuthenticationManager;
 
        


        public UsersController(ComputersExplorerContext _context, IGUIDAuthenticationManager _GUIDAuthenticationManager)
        {
            context = _context;
            GUIDAuthenticationManager = _GUIDAuthenticationManager;
        }

        // GET: api/Users
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            var token = GUIDAuthenticationManager.Authenticate(user.UserName, user.Password, context);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            context.Entry(user).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        // POST: api/Users
        [Route("Registration")]
        [HttpPost]
        public async Task<ActionResult<User>> UserRegistration(User user)
        {
            if (context.Roles.Where(role => role.Name == user.Role.Name).Count() == 0)
            {
                Results.BadRequest();
            }

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }







        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return context.Users.Any(e => e.Id == id);
        }
    }
}
