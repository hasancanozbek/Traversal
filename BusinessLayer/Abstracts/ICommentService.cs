using BusinessLayer.Dtos.Comment;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ICommentService
    {
        List<CommentDto> GetCommentDtoListOfCustomer(int customerId);
        IQueryable<Comment> GetAllCommentList();
        List<CommentDto> GetAllCommentDtoList();
        CommentDto GetCommentDtoById(int id);
    }
}
