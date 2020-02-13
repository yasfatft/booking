using Booking.Models;
using System.Linq;

namespace Booking.Services {
    public class ShowService {
        public AppDbContext _appDbContext { get; set; }

        public ShowService(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
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

        public void save(Show show) {
            _appDbContext.shows.Add(show);
            _appDbContext.SaveChanges();
        }
    }
}