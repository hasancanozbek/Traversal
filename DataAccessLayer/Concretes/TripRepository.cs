
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class TripRepository : GenericRepository<Trip>
    {
        public TripRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
