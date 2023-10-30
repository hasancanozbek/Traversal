using BusinessLayer.Dtos.Comments;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ICommentService
    {
        DataResult<List<CommentDto>> GetCommentDtoListOfCustomer(int customerId);
        DataResult<IQueryable<Comment>> GetAllCommentList();
        DataResult<List<CommentDto>> GetAllCommentDtoList();
        Task<DataResult<CommentDto>> GetCommentDtoById(int id);
        Task<Result> AddComment(AddCommentDto comment);
        Task<DataResult<CommentDto>> UpdateComment(UpdateCommentDto comment, int commentId);
        Task<Result> DeleteComment(CommentDto comment);
    }
}
