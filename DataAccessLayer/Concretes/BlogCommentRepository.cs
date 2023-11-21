using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class BlogCommentRepository : GenericRepository<BlogComment>, IBlogCommentRepository
    {
        public BlogCommentRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
