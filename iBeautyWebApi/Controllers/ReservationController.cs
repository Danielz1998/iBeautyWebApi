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
    public class ReservationController : Controller
    {
        private readonly iBeautyContext _context;

        public ReservationController(iBeautyContext context)
        {
            _context = context;
        }

        [HttpGet("GetReservation/{id}")]
        public async Task<ActionResult<Reservations>> GetRservation(int id)
        {
            var reservation = _context.Reservations
                .Include(serv => serv.Service)
                .Include(serv => serv.Service).ThenInclude(Sal => Sal.Salon)
                .Where(reserv => reserv.ReservationId == id).Select(Service => new 
                { 
                    Salon = Service.Service.Salon.Name,
                    Service = Service.Service.Name,
                    Price = Service.Service.Price,
                    Date = Service.Date,
                    Status = Service.Status,
                }).ToListAsync();

            var validar = reservation == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpGet("GetReservations")]
        public async Task<ActionResult<Reservations>> GetRservations(int id)
        {
            var reservation = _context.Reservations
                .Include(serv => serv.Service)
                .Include(serv => serv.Service).ThenInclude(Sal => Sal.Salon)
                .Where(reserv => reserv.UserId == id)
                .Select(Service => new
                {
                    Salon = Service.Service.Salon.Name,
                    Service = Service.Service.Name,
                    Price = Service.Service.Price,
                    Date = Service.Date,
                    Status = Service.Status,
                }).ToListAsync();

            var validar = reservation == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<Reservations>> PostAddress(Reservations reserv)
        {
            Reservations item = new Reservations()
            {
                UserId = reserv.UserId,
                ServiceId = reserv.ServiceId,
                Date = reserv.Date,
                Status = false,
                DateAdded = DateTime.Now
            };

            _context.Reservations.Add(item);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                ReservationId = item.ReservationId,
                UserId = item.UserId,
                ServiceId = item.ServiceId,
                Date = item.Date,
                Status = item.Status,
                DateAdded = item.DateAdded
            });
        }
    }
}
