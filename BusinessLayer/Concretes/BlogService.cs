using BusinessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class BlogService : IBlogService
    {
        public IQueryable<Blog> GetAllBlogList()
        {
            return null;
        }
    }
}
