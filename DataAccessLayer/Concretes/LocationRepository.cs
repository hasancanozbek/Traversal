
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
