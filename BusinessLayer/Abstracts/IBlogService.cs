using BusinessLayer.Dtos.Blogs;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface IBlogService
    {
        DataResult<List<BlogDto>> GetAllBlogList(bool includePassives = false);
        DataResult<IQueryable<Blog>> GetAllBlogsAsQueryable(bool tracking = false);
        Task<DataResult<BlogDto>> GetBlogById(int blogId);
        Task<DataResult<BlogDto>> UpdateBlog(UpdateBlogDto blog, int blogId);
        Task<Result> AddBlog(AddBlogDto blog);
        Task<Result> DeleteBlog(BlogDto blog);
        Task SetActive(Blog entity, bool isActive);
    }
}
