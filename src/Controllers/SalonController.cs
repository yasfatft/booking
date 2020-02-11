using Microsoft.AspNetCore.Mvc;
using Booking.Models;
namespace Booking.Controllers
{
    [Route ("/salons")]
    [ApiController]
    public class SalonController:Controller
    {
        private  AppDbContext _context;
        public SalonController(AppDbContext context)
        {
            _context=context;

        }
        // [HttpPut]
        // public ActionResult CreateSalon([FromBody]Salon salon)
        // {
        //     _context.Salons.Add(salon);
        //     _context.SaveChanges();
        //     return Ok();

        // }

    }
}