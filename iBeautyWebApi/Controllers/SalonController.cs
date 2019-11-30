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
            var stores = await _context.SP_NearbySalons.FromSql($"sp_NearbySalons {lat}, {lon}").OrderBy(x => x.Distance).ToListAsync();

            return Ok(stores);
        }
    }
}
