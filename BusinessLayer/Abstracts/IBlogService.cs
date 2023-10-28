using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface IBlogService
    {
        public IQueryable<Blog> GetAllBlogList();
    }
}
