using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Booking.Models;
using Booking.Services;
using System;

namespace Booking.Controllers
{
    [Route("api/v1/shows")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        public ShowService _showService { get; set; }

        public ShowController(ShowService showService) {
            _showService = showService;
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

            if (!_showService.isSalonAvailable(show.SalonId)) {
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

            _showService.save(show);
            return Created("show created",show);
        }
    }
}