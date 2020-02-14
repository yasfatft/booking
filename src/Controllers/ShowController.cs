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
        public IActionResult post([FromBody]RequestShow requestShow) {
            
            
            if (requestShow == null)
            {
                return BadRequest("Null json");
            }

            if (requestShow.StartTime == null)
            {
                return BadRequest("Null StartTime");
            }

            if (requestShow.EndTime == null)
            {
                return BadRequest("Null EndTime");
            }

            if (requestShow.Title == null) 
            {
                return BadRequest("Null Title");
            }

            if (requestShow.Summary == null) 
            {
                return BadRequest("Null Summary");
            }

            if (requestShow.Price == null || requestShow.Price < 0) 
            {
                return BadRequest("Null Price or negetive Price");
            }

            if (requestShow.SalonId == null) 
            {
                return BadRequest("Null SalonId");
            }

            if (!isSalonAvailable(requestShow.SalonId ?? -1)) 
            {
                return NotFound("Unavailable SalonId");
            }
                    
            if (requestShow.StartTime <= DateTime.Now) 
            {
                return BadRequest("StartTime is in past");
            }

            if (requestShow.StartTime >= requestShow.EndTime) 
            {
                return BadRequest("Endtime is before than StartTime");
            }
            

            const int maxSummaryLength = 250;
            const int maxTitleLength = 40;
            if (requestShow.Title.Length > maxTitleLength)
            {
                return BadRequest(string.Format(@"Too long Title, Title must be less than {0} characters", maxTitleLength));
            }
            if (requestShow.Summary.Length > maxSummaryLength)
            {
                return BadRequest(string.Format(@"Too long Summary, Summary must be less than {0} characters", maxSummaryLength));                
            }

            const int maxPrice = 100;
            if (requestShow.Price > maxPrice) {
                return BadRequest("Price is over the price threshold");
            }

            const int minShowTime = 30;
            const int maxShowTime = 120;
            TimeSpan showLength = new TimeSpan();
            if ( (requestShow.EndTime - requestShow.StartTime).HasValue )
            {
                showLength = ((requestShow.EndTime - requestShow.StartTime)).Value;
            }
            int showLengthAsMinute = showLength.Minutes;
            if (showLengthAsMinute < minShowTime)
            {
                return BadRequest("Show period is under minShowTime threshold");
            }
            if (showLengthAsMinute > maxShowTime){

                return BadRequest("Show period is over maxShowTime threshold");
            }   

            Show show = RequestShowToShowConverter(requestShow);
    
            _appDbContext.shows.Add(show);
            _appDbContext.SaveChanges();
            return Ok(string.Format("Show created {0}",requestShow));
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
        public Show RequestShowToShowConverter(RequestShow requestShow){
            Show show =new Show();
            show.StartTime = requestShow.StartTime ?? DateTime.Now;
            show.EndTime = requestShow.EndTime ?? DateTime.Now;  
            show.Title = requestShow.Title; 
            show.Summary = requestShow.Summary;
            show.Price = requestShow.Price ?? -1;
            show.SalonId = requestShow.SalonId ?? -1;    
            return show;     
        }
    }

    public class RequestShow
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public int? Price { get; set; }
        public int? SalonId { get; set; 
        }
    }

}