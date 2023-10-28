using BusinessLayer.Abstracts;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public IQueryable<Location> GetAllLocationList()
        {
            return locationRepository.GetAll();
        }

        public Task<Location> GetLocationById(int locationId)
        {
            return locationRepository.GetByIdAsync(locationId);
        }
    }
}
