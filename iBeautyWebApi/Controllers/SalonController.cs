using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBeautyWebApi.Models.Stored_Procedures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iBeautyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : Controller
    {
        private readonly iBeautyContext _context;

        public SalonController(iBeautyContext context)
        {
            _context = context;
        }

        [HttpGet("NearbySalons/{latitude}/{longitude}")]
        public async Task<ActionResult<sp_NearbySalons>> GetNearbyStores(string latitude, string longitude)
        {
            var lat = latitude;
            var lon = longitude;
            var salons = await _context.SP_NearbySalons.FromSql($"sp_NearbySalons {lat}, {lon}").Where(sal => sal.Status == true).OrderBy(x => x.Distance).ToListAsync();

            return Ok(salons);
        }

        [HttpGet("Salons")]
        public async Task<ActionResult<sp_NearbySalons>> GetSalons()
        {
            var salons = await _context.Salons.Where(sal => sal.Status == true).Select(Salon => new
            {
                SalonId = Salon.SalonId,
                Name = Salon.Name,
                Image = Salon.Image,
                Logo = Salon.Logo,
                Telephone = Salon.Telephone,
                Email = Salon.Email,
                Address = Salon.Address,
                Status = Salon.Status,
                Description = Salon.Description
            }).ToListAsync();

            return Ok(salons);
        }

        [HttpGet("Salons/{id}")]
        public async Task<ActionResult<sp_NearbySalons>> GetSalonsById(int id)
        {
            var salons = await _context.Salons.Where(sal => sal.SalonId == id && sal.Status == true).Select(Salon => new
            {
                SalonId = Salon.SalonId,
                Name = Salon.Name,
                Image = Salon.Image,
                Logo = Salon.Logo,
                Telephone = Salon.Telephone,
                Email = Salon.Email,
                Address = Salon.Address,
                Status = Salon.Status,
                Description = Salon.Description
            }).ToListAsync();

            return Ok(salons);
        }


    }
}
