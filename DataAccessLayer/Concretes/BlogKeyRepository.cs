using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class BlogKeyRepository : GenericRepository<BlogKey>, IBlogKeyRepository
    {
        public BlogKeyRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
