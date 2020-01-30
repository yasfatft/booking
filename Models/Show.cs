using System;
namespace Domain.Models
{
    public class Show
    {   
        public int Id { get; set; }
        
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public String Summary { get; set; }

        public int Price { get; set; }

        public int SalonId { get; set; }
        public int Salon { get; set; }
    }
}