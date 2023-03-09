using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputersExplorer;
using ComputersExplorer.DBO;
using ComputersExplorer.CustomAuthenticationSchemes.GUID;
using Microsoft.AspNetCore.Authorization;
using ComputersExplorer.DTO;
using ComputersExplorer.Logic;

namespace ComputersExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGUIDAuthenticationManager GUIDAuthenticationManager;
        private readonly ILogger<UsersController> logger;
        private readonly UserLogicProvider userLogicProvider;
        private readonly RoleLogicProvider roleLogicProvider;
 
        
        
        public UsersController(IGUIDAuthenticationManager _GUIDAuthenticationManager, ILogger<UsersController> _logger, UserLogicProvider _userLogicProvider, RoleLogicProvider _roleLogicProvider)
        {
            GUIDAuthenticationManager = _GUIDAuthenticationManager;
            logger = _logger;
            userLogicProvider = _userLogicProvider;
            roleLogicProvider = _roleLogicProvider;
        }


        /// <summary>
        /// Метод для получения списка пользователей.Uri: api/Users/GetUsers
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        [HttpGet("GetUsers")]
        public async Task<IEnumerable<Users>> GetUsers()
        {
            logger.LogInformation("GetUsersTriggered");

            var users = userLogicProvider.GetUsers();

            return users;
        }

        /// <summary>
        /// Метод для аутентификации пользователя. Uri: api/users/login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (!userLogicProvider.isUserWithThisCredentialsExist(user.UserName, user.Password)) return Unauthorized();
            int UserRoleId = userLogicProvider.GetUserRoleIdByName(user.UserName);
            if (UserRoleId == -1) return Unauthorized();

            var UserRoleName = roleLogicProvider.GetRoleById(UserRoleId).Name;

            //Запрос GUID-токена аутентификации на основе данных пользователя
            var token = GUIDAuthenticationManager.Authenticate(UserRoleName, user.UserName);

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
            var role = roleLogicProvider.FindRoleByName(user.Role.Name);
            if (role is null)
            {
                return BadRequest();
            }
            
            //Назначение роли через навигационное свойство
            user.Role = role;

            //Если в БД еще нет пользователя с таким именем
            if (!userLogicProvider.isUserWithThisCredentialsExist(user.UserName))      
            {
                userLogicProvider.AddUser(user);
                await userLogicProvider.SaveChanges();
                return CreatedAtAction("registration", new { id = user.Id }, user);
            }

            return BadRequest();
        }
  

        /// <summary>
        /// Функция удаления пользователя. Uri: api/Users/DeleteUser/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = userLogicProvider.GetUserById(id);
            
            if (user == null)
            {
                return NotFound();
            }
            userLogicProvider.DeleteUser(user);
            await userLogicProvider.SaveChanges();

            return NoContent();
        }
    }
}
