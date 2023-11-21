using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class TripDateRepository : GenericRepository<TripDate>, ITripDateRepository
    {
        public TripDateRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
