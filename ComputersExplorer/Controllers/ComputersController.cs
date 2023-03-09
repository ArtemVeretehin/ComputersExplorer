using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputersExplorer;
using ComputersExplorer.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ComputersExplorer.DTO;
using ComputersExplorer.Pagination;
using ComputersExplorer.Logic;

namespace ComputersExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly ComputersExplorerContext context;
        private readonly ComputerLogicProvider computerLogicProvider;
        private readonly UserLogicProvider userLogicProvider;

        public ComputersController(ComputersExplorerContext _context, ComputerLogicProvider _computerLogicProvider, UserLogicProvider _userLogicProvider)
        {
            context = _context;
            computerLogicProvider = _computerLogicProvider;
            userLogicProvider = _userLogicProvider;
        }


        /// <summary>
        /// Получение списка компьютеров. Uri: api/Computers/GetComputers
        /// </summary>
        /// <param name="paginationData"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetComputers")]
        public async Task<IEnumerable<Computers>> GetComputers([FromQuery] PaginationData paginationData)
        { 
            //Если роль "User" - вывести компьютеры асоциированные с этим пользователем
            if (HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "User")
            {
                var userName = HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;


                var computers = userLogicProvider.GetUserWithInclude(x => x.Computers.Skip((paginationData.PageNumber - 1) * paginationData.PageSize).Take(paginationData.PageSize))
                .FirstOrDefault(u => u.UserName == userName)?.Computers;
           

                var dto_computers = computers?.Select(c => new Computers(c.Id, c.Name, c.UserId));

                return dto_computers;
            }

            //Роль "Admin" - вывести все компьютеры
            return context.Computers.Skip((paginationData.PageNumber - 1) * paginationData.PageSize).Take(paginationData.PageSize).Select(c=> new Computers(c.Id, c.Name, c.UserId));
        }




        /// <summary>
        /// Изменение данных компьютера.Uri: api/Computers/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="computer"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("ComputerModify/{id}")]
        public async Task<IActionResult> PutComputer(int id, Computer computer)
        {
            if (id != computer.Id)
            {
                return BadRequest();
            }

            var Computer = computerLogicProvider.GetComputerById(id);

            //Если роль "User" - проверяется является ли компьютер асоциированным с данным пользователем. Если нет, то доступ запрещается
            if (HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "User")
            {
                var userName = HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                var userId = userLogicProvider.GetUserByName(userName).Id;

                if (Computer.UserId != userId) return Forbid();
                
            }


            Computer.Name = (computer.Name is not null) ? computer.Name : Computer.Name;
            Computer.UserId = (computer.UserId is not null) ? computer.UserId : Computer.UserId;


            try
            {
                await computerLogicProvider.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Метод для добавления компьютера. Uri: api/Computers/AddComputer
        /// </summary>
        /// <param name="computer"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddComputer")]
        public async Task<ActionResult<Computer>> AddComputer(Computer computer)
        {
            //Получение сущности пользователя на основе данных аутентификации и сопоставление пользователя с добавляемым компьютером
            var userName = HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;   
 
            var user = userLogicProvider.GetUserByName(userName);
            computer.User = user;
            computerLogicProvider.AddComputer(computer);
            computerLogicProvider.SaveChanges();
     

            return CreatedAtAction("AddComputer", new { id = computer.Id }, computer);
        }

        /// <summary>
        /// Метод для удаления компьютера. Uri: api/Computers/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteComputer/{id}")]
        public async Task<IActionResult> DeleteComputer(int id)
        {
            var computer = computerLogicProvider.GetComputerById(id);
            if (computer is null)
            {
                return NotFound();
            }

            computerLogicProvider.DeleteComputer(computer);
            computerLogicProvider.SaveChanges();

            return NoContent();
        }

        private bool ComputerExists(int id)
        {
            return context.Computers.Any(e => e.Id == id);
        }
    }
}
