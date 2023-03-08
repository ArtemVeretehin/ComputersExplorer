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
using ComputersExplorer.DTO;

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


        /// <summary>
        /// Метод для получения списка пользователей.Uri: api/Users
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IEnumerable<Users>> GetUsers()
        {
            return context.Users.Select(u => new Users(u.Id, u.UserName, u.Password, u.RoleId));
        }

        /// <summary>
        /// Метод для аутентификации пользователя. Uri: api/users/login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var token = GUIDAuthenticationManager.Authenticate(user.UserName, user.Password, context);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }



        /// <summary>
        /// Метод для регистрации пользователя. Uri: api/users/registration
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("registration")]
        public async Task<ActionResult<User>> Registration(User user)
        {
            //Если задана роль, которой нет в БД
            if (context.Roles.Where(role => role.Name == user.Role.Name).Count() == 0)
            {
                Results.BadRequest();
                return CreatedAtAction("GetUser", null, null);
            }
            
            //Назначение роли через навигационное свойство
            user.Role = context.Roles.FirstOrDefault(role => role.Name == user.Role.Name);

            //Если уже нет пользователя с таким именем
            if (context.Users.Where(u => u.UserName == user.UserName).Count() == 0)
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }

            return CreatedAtAction("GetUser", null, null);
        }
  

        /// <summary>
        /// Функция удаления пользователя. Uri: api/Users/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser/{id}")]
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
    }
}
