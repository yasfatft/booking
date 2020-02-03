using System.Reflection.Emit;
using System.Collections.Generic;
namespace cinemaproject.Models
{
    public class Seat
    {
        public  int id{get; set;}
        public  double x{get; set;}
        public  double y{get; set;}
        public  int sallon_id{get; set;}
        public Sallon sallon{get; set;}
        
    }
}