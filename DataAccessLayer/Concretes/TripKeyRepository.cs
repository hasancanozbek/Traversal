using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class TripKeyRepository : GenericRepository<TripKey>, ITripKeyRepository
    {
        public TripKeyRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
