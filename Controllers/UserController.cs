using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FundStarDemoAPI.Data;
using FundStarDemoAPI.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using FundStarDemoAPI.Services;
//using FundStarDemoAPI.Services;

namespace FundStarDemoAPI.Controllers
{
    [Route("v1/users")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            var users = await context
                .Users
                .AsNoTracking()
                .ToListAsync();

            return users;
        }

    
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
                    [FromServices] DataContext context,
                    [FromBody]User model)
        {
            var user = await context.Users
                .AsNoTracking()
                .Where(x => x.Email == model.Email && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            // Esconde a senha
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost]
        [Route("signup")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> CreateNewUser(
                    [FromServices] DataContext context,
                    [FromBody]User model)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if(user != null)
                return BadRequest(new { message = "This e-mail is already registered."});


            if(ModelState.IsValid){
                context.Users.Add(model);
                await context.SaveChangesAsync();
            }
                            
            return new
            {
                model = model,
            };

            
          
        }        
    }
}