using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DotnetRedisApi;


[ApiController]
[Route("api/[controller]")]
public class ProductsController(RedisCacheService redis, ProductRepository productRepository) :ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Product>> GetProduct(int productId)
    {
         var product = redis.Get<Product>($"product:-{productId}");
        if (product != null)
        {
            return Ok(product);
        }
        else
        {
            product = productRepository.GetProduct(productId);
            if (product == null)
            {
                return NotFound();
            }
            redis.Set($"product:-{productId}", product);
            return Ok(product);
        }
    }


    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        productRepository.AddProduct(product);
        return Ok();
    }
}
