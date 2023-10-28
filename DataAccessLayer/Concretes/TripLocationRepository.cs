
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class TripLocationRepository : GenericRepository<TripLocation>, ITripLocationRepository
    {
        public TripLocationRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
