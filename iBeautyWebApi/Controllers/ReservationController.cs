using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iBeautyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly iBeautyContext _context;

        public ReservationController(iBeautyContext context)
        {
            _context = context;
        }

        [HttpPost("postReservation")]
        public async Task<ActionResult<Reservations>> PostReservation(Reservations reserv)
        {
            bool reservExists = _context.Reservations.Any(x => x.UserId == reserv.UserId && x.Date == reserv.Date);
            if (reservExists)
            {
                return BadRequest();
            }

            Reservations item = new Reservations()
            {
                UserId = reserv.UserId,
                ServiceId = reserv.ServiceId,
                Date = reserv.Date,
                ReservationStatusId = 1,
                DateAdded = DateTime.Now
            };

            _context.Reservations.Add(item);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                ReservationId = item.ReservationId,
                UserId = item.UserId,
                User = _context.Users.FirstOrDefault(x => x.UserId == item.UserId).Firstname ?? "",
                ServiceId = item.ServiceId,
                Service = _context.Services.FirstOrDefault(x => x.ServiceId == item.ServiceId).Name ?? "",
                Date = item.Date,
                ReservationStatusId = item.ReservationStatusId,
                DateAdded = item.DateAdded
            });
        }

        [HttpGet("getReservations/{userid}")]
        public async Task<ActionResult<IEnumerable<Reservations>>> GetRservations(int userid)
        {

            var reservations = await _context.Reservations.Include(x => x.User).Include(x => x.Service).Include(x => x.ReservationStatus).Where(x => x.UserId == userid).Select(reserv => new
            {
                ReservationId = reserv.ReservationId,
                UserId = reserv.UserId,
                User = reserv.User.Firstname,
                ServiceId = reserv.ServiceId,
                Service = reserv.Service.Name,
                Date = reserv.Date.ToString("dd MMMM", CultureInfo.InvariantCulture),
                DateHour = reserv.Date.ToString("h:mm tt", CultureInfo.InvariantCulture),
                ReservationStatusId = reserv.ReservationStatusId,
                ReservationStatus = reserv.ReservationStatus.Name
            }).ToListAsync();

            var validar = reservations == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(reservations);
        }
    }
}
