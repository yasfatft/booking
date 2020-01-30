using System;
namespace cinemaproject.Models
{
    public class Show
    {
        public int id { get; set; }
        public string title{ get; set; }
        public DateTime start_time{ get; set; }
        public DateTime end_time{ get; set; }
        public int price{ get; set; }
        public int sallon_id{ get; set; }
        public Sallon sallon{get; set;}
    }
}