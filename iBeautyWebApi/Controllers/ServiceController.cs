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
    public class ServiceController : Controller
    {
        private readonly iBeautyContext _context;

        public ServiceController(iBeautyContext context)
        {
            _context = context;
        }

        [HttpGet("GetService/{id}")]
        public async Task<ActionResult<Services>> GetService(int id)
        {
            var service = _context.Services
                .Include(cat => cat.Category)
                .FirstOrDefault(serv => serv.ServiceId == id && serv.Status == true);

            var validar = service == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(new
            {
                ServiceId = service.ServiceId,
                Name = service.Name,
                Description = service.Description,
                Category = service.Category.Name,
                Price = service.Price,
                Image = service.Image
            });
        }

        [HttpGet("ServicesbyCategory/{id}")]
        public async Task<ActionResult> GetProducts(int id)
        {
            var categories = await _context.Categories.Where(cat => cat.SalonId == id && cat.Status == true)
                .Include(sal => sal.Salon)
                .Select(Category => new
                {
                    SalonId = Category.SalonId,
                    Salon = Category.Salon.Name,
                    CategoryName = Category.Name,
                    Services = _context.Services.Where(serv => serv.CategoryId == Category.CategoryId && serv.Status == true)
                .Include(cat => cat.Category)
                .Select(Service => new
                {
                    ServiceId = Service.ServiceId,
                    Name = Service.Name,
                    Description = Service.Description,
                    CategoryName = Service.Category.Name,
                    Price = Service.Price,
                    Image = Service.Image
                })
                }).ToListAsync();
            return Ok(categories);
        }
    }
}
