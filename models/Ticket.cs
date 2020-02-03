using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaproject.Models
{
    public class Ticket
    {
        public int show_id { get; set; }
        public Show show{get; set;}
        public int seat_id { get; set; }
        public Seat seat{get; set;}
        public int user_id { get; set; }    
    }
}