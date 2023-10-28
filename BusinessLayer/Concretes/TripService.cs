using BusinessLayer.Abstracts;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class TripService : ITripService
    {
        private readonly ITripRepository tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }

        public IQueryable<Trip> GetAllTripList()
        {
            var tripList = tripRepository.GetAll();
            return tripList;
        }

        public Task<Trip> GetTripById(int tripId)
        {
            var trip = tripRepository.GetByIdAsync(tripId);
            return trip;
        }
    }
}
