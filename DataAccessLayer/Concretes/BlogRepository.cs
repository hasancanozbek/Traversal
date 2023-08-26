
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class BlogRepository : GenericRepository<Blog>
    {
        public BlogRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
