using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FundStarDemoAPI.Data;
using FundStarDemoAPI.Models;

namespace FundStarDemoAPI.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {

            var product1 = new Product { Id = 1, Name = "Arroz",  Price = 399, Quantity = 1000 };
            var product2 = new Product { Id = 2, Name = "Feijão",  Price = 270, Quantity = 1000 };
            var product3 = new Product { Id = 3, Name = "Macarrão",  Price = 400, Quantity = 250 };
            var product4 = new Product { Id = 4, Name = "Farinha",  Price = 500, Quantity = 200 };
            var product5 = new Product { Id = 5, Name = "Açúcar",  Price = 299, Quantity = 500 };
            var product6 = new Product { Id = 6, Name = "Óleo Soja",  Price = 899, Quantity = 100 };
            
            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);
            context.Products.Add(product4);
            context.Products.Add(product5);
            context.Products.Add(product6);

            var user = new User { Id = 1, Email = "123",  Role="teste", Password = "1", Name ="Gabriel" };

            context.Users.Add(user);

            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    }
}