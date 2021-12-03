using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Product : Controller
    {
        private readonly StoreContext _context;
        public Product(StoreContext storeContext)
        {
            _context = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{i}")]
        public async Task<ActionResult<Product>> GetProduct(int i)
        {
            return Ok(await _context.Products.FindAsync(i));
        }
    }
}
