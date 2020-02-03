using System.Collections.Generic;
namespace cinemaproject.models
{
    public class Sallon
    {
        public string name{get; set;}
        public double seatHeight { get; set;}
        public double seatWidth { get; set;}
        public int id { get; set; }
        public ICollection<Seat> seats{ get; set; }
    }
}
