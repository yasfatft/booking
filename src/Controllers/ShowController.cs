using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Booking.Models;
using System;
using System.Linq;

namespace Booking.Controllers
{
    [Route("api/v1/shows")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        public AppDbContext _appDbContext { get; set; }

        public ShowController(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public IActionResult post([FromBody]Show show) {
            
            if (show == null) {
                Console.WriteLine("1");
                return BadRequest();
            }

            if (show.EndTime == null || show.StartTime == null || show.Title == null ||
                show.Summary == null || show.Price < 0) 
            {
                Console.WriteLine("2");
                return BadRequest();
            }

            if (show.StartTime <= DateTime.Now) {
                Console.WriteLine("3");
                return Conflict();
            }

            if (show.StartTime >= show.EndTime) {
                return BadRequest();
            }

            if (!isSalonAvailable(show.SalonId)) {
                Console.WriteLine("4");
                return Conflict();
            }

            if (show.Summary.Length > 250 || show.Title.Length > 40) {
                Console.WriteLine("5");
                return BadRequest();
            }

            int maxPrice = 100;
            if (show.Price > maxPrice) {
                Console.WriteLine("6");
                return BadRequest();
            }

            int minShowTime = 30;
            int maxShowTime = 120;
            int showLenght = (show.EndTime - show.StartTime).Minutes;
            if (showLenght < minShowTime || showLenght > maxShowTime){
                Console.WriteLine("7");
                return BadRequest();
            }   

            _appDbContext.shows.Add(show);
            _appDbContext.SaveChanges();
            return Created("show created",show);
        }

        public bool isSalonAvailable(int salonId) {
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