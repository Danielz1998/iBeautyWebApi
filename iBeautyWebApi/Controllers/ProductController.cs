using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iBeautyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly iBeautyContext _context;

        public ProductController(iBeautyContext context)
        {
            _context = context;
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var product = _context.Products
                .Include(cat => cat.Category)
                .FirstOrDefault(x => x.ProductId == id);

            var validar = product == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(new
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Descripcion = product.Description,
                Category = product.Category.Name,
                Precio = product.Price,
                Imagen = product.Image
            });
        }

        [HttpGet("ProductsbyCategory/{id}")]
        public async Task<ActionResult> GetProducts(int id)
        {
            var categories = await _context.Categories.Where(cat => cat.SalonId == id)
                .Include(sal => sal.Salon)
                .Select(Category => new
                {
                    SalonId = Category.SalonId,
                    Salon = Category.Salon.Name,
                    CategoryName = Category.Name,
                    Products = _context.Products.Where(prod => prod.CategoryId == Category.CategoryId)
                .Include(cat => cat.Category)
                .Select(Product => new
                {
                    ProductId = Product.ProductId,
                    Name = Product.Name,
                    Description = Product.Description,
                    CategoryName = Product.Category.Name,
                    Price = Product.Price,
                    Image = Product.Image
                })
                }).ToListAsync();
            return Ok(categories);
        }
    }
}
