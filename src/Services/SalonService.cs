using Booking.Models;
using System.Linq;

namespace Booking.Services {
    public class SalonService {
        public AppDbContext _appDbContext { get; set; }

        public SalonService(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public bool isSalonIdDuplicate(int salonId) {
            var salonIds = _appDbContext.salons.Select(s => s.Id);
            foreach (int id in salonIds) {
                if (salonId == id) {
                    return true;
                }
            }
            return false;
        }
        
        public void save(Salon Salon) {
            _appDbContext.salons.Add(Salon);
            _appDbContext.SaveChanges();
        }
    }
}