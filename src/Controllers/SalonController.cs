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
        public IActionResult post([FromBody]Salon salon) {
            
            
            if (salon == null) {
                return BadRequest();
            }

            if (salon.Name == null || salon.Name.Equals("") || salon.SeatHeight <= 0 || salon.SeatWidth <= 0) {
                return BadRequest();
            }

            if (isSalonIdDuplicate(salon.Id)){
                return Conflict();
            }

            _appDbContext.salons.Add(salon);
            _appDbContext.SaveChanges();
            return Created("salon created",salon);
        }

         public bool isSalonIdDuplicate(int salonId) {
            var salonIds = _appDbContext.salons.Select(s => s.Id);
            foreach (int id in salonIds) {
                if (salonId == id) {
                    return true;
                }
            }
            return false;
        }
    }
}