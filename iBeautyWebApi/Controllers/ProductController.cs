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
                .FirstOrDefault(pro => pro.ProductId == id && pro.Status == true);

            var validar = product == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(new
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                CategoryName = product.Category.Name,
                Price = product.Price,
                Image = product.Image
            });
        }

        [HttpGet("ProductsbyCategory/{id}")]
        public async Task<ActionResult> GetProducts(int id)
        {
            var categories = await _context.Categories.Where(cat => cat.SalonId == id && cat.Status == true)
                .Include(sal => sal.Salon)
                .Select(Category => new
                {
                    SalonId = Category.SalonId,
                    SalonName = Category.Salon.Name,
                    CategoryName = Category.Name,
                    Products = _context.Products.Where(prod => prod.CategoryId == Category.CategoryId && prod.Status == true)
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

            var validar = categories == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpPost("PostProduct")]
        public async Task<ActionResult<Products>> PostProduct(Products prod)
        {
            Products item = new Products()
            {
                Name = prod.Name,
                Description = prod.Description,
                Image = prod.Image,
                CategoryId = prod.CategoryId,
                SalonId = prod.SalonId,
                Price = prod.Price,
                Status = prod.Status,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            };

            _context.Products.Add(item);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Name = item.Name,
                Description = item.Description,
                Image = item.Image,
                CategoryId = item.CategoryId,
                SalonId = item.SalonId,
                Price = item.Price,
                Status = item.Status,
                DateAdded = item.DateAdded,
                DateModified = item.DateModified
            });
        }

        [HttpPut("PutProduct/{id}")]
        public async Task<IActionResult> PutProduct(int id, Products prod)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);

            var validar = false;
            if (validar)
            {
                return NotFound();
            }

            product.Name = prod.Name;
            product.Description = prod.Description;
            product.Image = prod.Image;
            product.Price = prod.Price;
            product.Status = prod.Status;
            product.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Status = product.Status,
                DateModified = product.DateModified
            });
        }
    }
}
