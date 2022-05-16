using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FundStarDemoAPI.Data;
using FundStarDemoAPI.Models;

namespace FundStarDemoAPI.Controllers
{
    [Route("v1/order")]
    public class OrderController : Controller
    {

        [HttpGet]
        [Route("")]
    
        public async Task<ActionResult<List<Order>>> Get([FromServices] DataContext context)
        {
            var orders = await context.Orders.AsNoTracking().ToListAsync();
            return orders;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Order>> Post(
            [FromServices] DataContext context,
            [FromBody]Order model){

            if (ModelState.IsValid)
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == int.Parse(model.ProductsList));
                product.Quantity -= model.OrderQuantity;
                
                context.Orders.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);       
            }
                return BadRequest(new { message = "API Body is invalid." });
            }

    }
}