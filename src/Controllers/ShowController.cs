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
                return BadRequest();
            }

            if (show.EndTime == null || show.StartTime == null || show.Title == null ||
                show.Summary == null || show.Price < 0) 
            {
                return BadRequest();
            }

            if (show.StartTime >= show.EndTime || show.StartTime <= DateTime.Now) {
                return BadRequest();
            }

            if (!_showService.isSalonAvailable(show.SalonId)) {
                return Conflict();
            }

            if (show.Summary.Length > 250 || show.Title.Length > 40) {
                return BadRequest();
            }

            int maxPrice = 100;
            if (show.Price > maxPrice) {
                return BadRequest();
            }

            int minShowTime = 30;
            int maxShowTime = 120;
            int showLenght = (show.EndTime - show.StartTime).Minutes;
            if (showLenght < minShowTime || showLenght > maxShowTime){
                return BadRequest();
            }

            _showService.save(show);
            return Created("show created",show);
        }
    }
}