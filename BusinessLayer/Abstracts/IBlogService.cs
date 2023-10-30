using BusinessLayer.Dtos.Blogs;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface IBlogService
    {
        DataResult<List<BlogDto>> GetAllBlogList();
        DataResult<List<BlogDto>> GetBlogListByCustomerId(int customerId);
        DataResult<IQueryable<Blog>> GetAllBlogsAsQueryable();
        Task<DataResult<BlogDto>> GetBlogById(int blogId);
        Task<DataResult<BlogDto>> UpdateBlog(UpdateBlogDto blog, int blogId);
        Task<Result> AddBlog(AddBlogDto blog);
        Task<Result> DeleteBlog(BlogDto blog);
    }
}
