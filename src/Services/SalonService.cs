using Booking.Models;

namespace Booking.Services {
    public class SalonService {
        public AppDbContext _appDbContext { get; set; }

        public SalonService(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public void save(Salon Salon) {
            _appDbContext.salons.Add(Salon);
            _appDbContext.SaveChanges();
        }
    }
}