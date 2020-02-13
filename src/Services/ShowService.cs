using Booking.Models;

namespace Booking.Services {
    public class ShowService {
        public AppDbContext _appDbContext { get; set; }

        public ShowService(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public bool isSalonAvailable(int salonId) {
            return true;
        }

        public void save(Show show) {
            _appDbContext.shows.Add(show);
            _appDbContext.SaveChanges();
        }
    }
}