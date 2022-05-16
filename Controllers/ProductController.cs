using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FundStarDemoAPI.Data;
using FundStarDemoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace FundStarDemoAPI.Controllers
{
    [Route("v1/products")]
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.AsNoTracking().ToListAsync();
            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post(
            [FromServices] DataContext context,
            [FromBody]Product model)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody]Product model)
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                    return NotFound(new { message = "Produto não encontrado" });

                try
                {
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Quantity = model.Quantity;
                    context.SaveChanges();
                    return product;
                }
                catch (Exception)
                {
                    return BadRequest(new { message = "Não foi possível alterar o produto" + product.Id });
                }
            }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Delete(
            [FromServices] DataContext context,
            int id)
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                    return NotFound(new { message = "Produto não encontrado" });

                try
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return product;
                }
                
                catch (Exception)
                {
                    return BadRequest(new { message = "Não foi possível deletar o produto" + product.Id });
                }
            }            

    }
}