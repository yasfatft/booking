using Booking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Booking.Controllers
{
    [Route("api/v1/salons")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        public AppDbContext _appDbContext { get; set; }

        public SalonController(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public IActionResult post([FromBody]RequestSalon requestSalon) {
            
            
            if (requestSalon == null) 
            {
                return BadRequest("Json should not be null");
            }

            if (string.IsNullOrEmpty(requestSalon.Name)) 
            {
                return BadRequest("Name should not be null or empty");
            }

            if (requestSalon.SeatHeight == null)
            {
                return BadRequest("SeatHeight should not be null");
            }

            if (requestSalon.SeatWidth == null)
            {
                return BadRequest("SeatWidth should not be null");
            }

            if (requestSalon.SeatHeight <= 0)
            {
                return BadRequest("SeatHeight should be positive");
            }
            
            if (requestSalon.SeatWidth <= 0)
            {
                return BadRequest("SeatWidth should be positive");
            }

            Salon salon = RequestSalonToSalonConverter(requestSalon);
            _appDbContext.salons.Add(salon);
            _appDbContext.SaveChanges();
            return Ok(string.Format("salon created",salon));
        }

        public Salon RequestSalonToSalonConverter(RequestSalon requestSalon){
            Salon salon = new Salon();
            salon.Name = requestSalon.Name;
            salon.SeatHeight = requestSalon.SeatHeight ?? -1;
            salon.SeatWidth = requestSalon.SeatWidth ?? -1;   
            return salon;     
        }
    }

        public class RequestSalon
    {
        public string Name { get; set; }
        public int? SeatWidth { get; set; }
        public int? SeatHeight { get; set; }
    }
}