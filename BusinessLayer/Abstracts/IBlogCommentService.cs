using BusinessLayer.Dtos.BlogComments;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface IBlogCommentService
    {
        DataResult<List<BlogCommentDto>> GetCommentListOfCustomerById(int customerId);
        DataResult<List<BlogCommentDto>> GetCommentListOfBlogById(int blogId);
        DataResult<IQueryable<BlogComment>> GetAllCommentsAsQueryable();
        DataResult<List<BlogCommentDto>> GetAllCommentList();
        Task<DataResult<BlogCommentDto>> GetCommentById(int id);
        Task<Result> AddComment(AddBlogCommentDto comment);
        Task<DataResult<BlogCommentDto>> UpdateComment(UpdateBlogCommentDto comment, int commentId);
        Task<Result> DeleteComment(BlogCommentDto comment);
        Result DeleteAllCommentOfBlog(int blogId);
    }
}
