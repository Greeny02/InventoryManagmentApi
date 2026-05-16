using InventoryManagementAPI.Models;
using InventoryManagementAPI.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("exists/{barcode}")]
        public async Task<IActionResult> CheckIfExists(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return BadRequest("Barcode is required");

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Barcode == barcode);

            if (product == null)
            {
                return Ok(new
                {
                    exists = false,
                    message = "Product not found"
                });
            }

            return Ok(new
            {
                exists = true,
                product.Id,
                product.Barcode,
                product.Name,
                product.Quantity
            });
        }

        [HttpPost("scan")]
        public async Task<IActionResult> Scan([FromBody] ScanRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Barcode))
                return BadRequest("Barcode is required");

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Barcode == request.Barcode);

            if (product == null)
            {
                // Optional: auto-create product
                var newProduct = new Product
                {
                    Barcode = request.Barcode,
                    Name = request.Name ?? "New Product",
                    Quantity = 1
                };

                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();

                return Ok(newProduct);
            }

            // Example: increase quantity
            product.Quantity += 1;
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}