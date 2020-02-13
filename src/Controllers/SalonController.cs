using Booking.Models;
using Booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Controllers
{
    [Route("api/v1/salons")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        public SalonService _salonService { get; set; }

        public SalonController(SalonService showService) {
            _salonService = showService;
        }

        [HttpPost]
        public IActionResult post([FromBody]Salon salon) {
            
            
            if (salon == null) {
                return BadRequest();
            }

            if (salon.Name == null || salon.Name.Equals("") || salon.SeatHeight <= 0 || salon.SeatWidth <= 0) {
                return BadRequest();
            }

            _salonService.save(salon);
            return Created("salon created",salon);
        }
    }
}