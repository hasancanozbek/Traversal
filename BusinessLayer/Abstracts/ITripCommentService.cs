using BusinessLayer.Dtos.Comments;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ITripCommentService
    {
        DataResult<List<TripCommentDto>> GetCommentListOfCustomerById(int customerId);
        DataResult<List<TripCommentDto>> GetCommentListOfTripById(int tripId);
        DataResult<IQueryable<TripComment>> GetAllCommentsAsQueryable();
        DataResult<List<TripCommentDto>> GetAllCommentList();
        Task<DataResult<TripCommentDto>> GetCommentById(int id);
        Task<Result> AddComment(AddTripCommentDto comment);
        Task<DataResult<TripCommentDto>> UpdateComment(UpdateTripCommentDto comment, int commentId);
        Task<Result> DeleteComment(TripCommentDto comment);
    }
}
