using System.Collections.Generic;
using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Booking.Models;
using System.Linq;
namespace Booking.Controllers
{
    [Route ("/show")]
    [ApiController]
    public class ShowsController:Controller
    {
        private readonly AppDbContext _context;
        public int MaximumPrice=100;
        public  TimeSpan MinimumTime  = new TimeSpan(0,30,0);
        public TimeSpan MaximumTime=new TimeSpan(2,0,0);
        public ShowsController(AppDbContext context)
        {
            _context =context;
        }
        [HttpPost]
        public ActionResult <string> CreateShow([FromBody]Show show)
        {  
           // var asgharShow = _context.show.Find(show.StartTime);
            var testShow =_context.Shows.Find(show.Id);
            if(testShow!=null)
            {
                return Conflict();
            }
            if(show.StartTime.Hour>=show.EndTime.Hour)
            {
                return BadRequest();
            }
            if(show.Price < 0)
            {
                return BadRequest();
            }
            if(show.StartTime<=DateTime.Now)
            {
                return BadRequest();
            }
            var salonId=_context.Salons.Find(show.SalonId);
            if(salonId==null)
            {
                return BadRequest();
            }
            char[] titleCharacters=show.Title.ToCharArray();
            if(titleCharacters.Length>10)
            {
                return BadRequest();
            }
            if(show.Price>MaximumPrice)
            {
                return BadRequest();
            }
            var showTime=show.EndTime-show.StartTime;
            if(showTime<MinimumTime||showTime>MaximumTime)
            {
                return BadRequest();

            }
            bool hasConflict = DefinedShowHaveConflict(show);
            if(hasConflict)
            {
                return Conflict();
            }
            _context.Shows.Add(show);
            _context.SaveChanges();
            return Ok();
        }
        


        public bool DefinedShowHaveConflict(Show show){

            IEnumerable<Show> query =
               from Var in _context.Shows.AsEnumerable()
               where ShowsTimeHaveConflict(Var,show)
               select Var;
            foreach(var VARIABLE in query)
            {
                if(VARIABLE.SalonId == show.SalonId)
                {
                        return true;
                }
            }
            return false;
        }
        public bool ShowsTimeHaveConflict([FromQuery]Show show1, Show show2)
        {
            if(show1.StartTime.Date==show2.StartTime.Date)
            {
                if(show1.StartTime.Hour<=show2.StartTime.Hour&&show2.StartTime<=show1.EndTime)
                {
                    return true;
                }
                if(show1.StartTime.Hour<=show2.EndTime.Hour&&show2.EndTime.Hour<=show1.EndTime.Hour)
                {
                    return true;
                }
                if(show1.StartTime.Hour<=show2.StartTime.Hour&& show1.EndTime.Hour>=show2.EndTime.Hour)
                {
                    return true;
                }
                if(show2.StartTime.Hour<=show1.StartTime.Hour&&show2.EndTime.Hour>=show1.EndTime.Hour)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;

            }
        }
        
    }
}