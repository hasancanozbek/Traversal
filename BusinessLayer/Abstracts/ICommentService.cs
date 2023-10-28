using BusinessLayer.Dtos.Comments;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ICommentService
    {
        List<CommentDto> GetCommentDtoListOfCustomer(int customerId);
        IQueryable<Comment> GetAllCommentList();
        List<CommentDto> GetAllCommentDtoList();
        Task<CommentDto> GetCommentDtoById(int id);
    }
}
