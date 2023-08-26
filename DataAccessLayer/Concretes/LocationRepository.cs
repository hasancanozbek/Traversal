
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class LocationRepository : GenericRepository<Location>
    {
        public LocationRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
