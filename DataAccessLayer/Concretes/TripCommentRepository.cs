using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class TripCommentRepository : GenericRepository<TripComment>, ITripCommentRepository
    {
        public TripCommentRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
