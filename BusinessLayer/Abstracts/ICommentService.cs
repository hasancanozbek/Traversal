
using BusinessLayer.Dtos;
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
