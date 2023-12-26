using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Comments;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concretes
{
    public class TripCommentService : ITripCommentService
    {
        private readonly ITripCommentRepository commentRepository;
        private readonly ITripDateService tripDateService;
        private readonly IMapper mapper;

        public TripCommentService(ITripCommentRepository commentRepository, IMapper mapper, ITripDateService tripDateService)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
            this.tripDateService = tripDateService;
        }

        public async Task<Result> AddComment(AddTripCommentDto comment)
        {
            var commentEntity = mapper.Map<TripComment>(comment);
            commentEntity.TripDateId = comment.TripId;
            await commentRepository.AddAsync(commentEntity);
            return new SuccessResult("Comment added");
        }

        public Result DeleteAllCommentOfTrip(int tripId)
        {
            var comments = GetCommentListOfTripById(tripId);
            if (comments.IsSuccess)
            {
                var commentList = mapper.Map<List<TripComment>>(comments.Data);
                commentRepository.RemoveRange(commentList);
                return new SuccessResult("Comments deleted");
            }
            return new ErrorResult("Comment couldn't deleted");
        }

        public async Task<Result> DeleteComment(TripCommentDto comment)
        {
            var commentEntity = mapper.Map<TripComment>(comment);
            await commentRepository.RemoveAsync(commentEntity);
            return new SuccessResult("Comment deleted");
        }

        public DataResult<List<TripCommentDto>> GetAllCommentList()
        {
            try
            {
                var result = GetAllCommentsAsQueryable();
                if (result.IsSuccess)
                {
                    var commentList = result.Data.Include(i => i.Customer).Include(i => i.TripDate.Trip).ToList();
                    var commentDtoList = mapper.Map<List<TripCommentDto>>(commentList);
                    return new SuccessDataResult<List<TripCommentDto>>(commentDtoList);
                }
                return new ErrorDataResult<List<TripCommentDto>>(null);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<TripCommentDto>>(exception.Message, null);
            }

        }

        public DataResult<IQueryable<TripComment>> GetAllCommentsAsQueryable()
        {
            var commentList = commentRepository.GetAll();
            return new SuccessDataResult<IQueryable<TripComment>>("All comments listed", commentList);
        }

        public async Task<DataResult<TripCommentDto>> GetCommentById(int id)
        {
            var comment = await commentRepository.GetWhere(s => s.Id == id).Include(i => i.Customer).Include(i => i.TripDate.Trip).FirstOrDefaultAsync();
            var commentDto = mapper.Map<TripCommentDto>(comment);
            commentDto.CustomerFirstName = comment.Customer.FirstName;
            commentDto.CustomerLastName = comment.Customer.LastName;
            commentDto.TripName = comment.TripDate.Trip.Title;
            commentDto.TripDate = comment.TripDate.Date;
            return new SuccessDataResult<TripCommentDto>("Comment information listed", commentDto);
        }

        public DataResult<List<TripCommentDto>> GetCommentListOfCustomerById(int customerId)
        {
            var commentList = mapper.Map<List<TripCommentDto>>(commentRepository.GetWhere(c => c.CustomerId == customerId).ToList());
            return new SuccessDataResult<List<TripCommentDto>>("All comments of customer listed", commentList);
        }

        public DataResult<List<TripCommentDto>> GetCommentListOfTripById(int tripId)
        {
            var tripDateResult = tripDateService.GetAllTripDaysOfTripById(tripId);
            if (tripDateResult.IsSuccess)
            {
                var tripDateIdList = tripDateResult.Data.Select(s => s.Id);
                var commentList = commentRepository.GetWhere(c => tripDateIdList.Contains(c.TripDateId)).Include(i => i.Customer).ToList();
                var commentDtoList = mapper.Map<List<TripCommentDto>>(commentList);
                commentDtoList.ForEach(commentDto =>
                {
                    commentDto.CustomerFirstName = commentList.First(s => s.Id == commentDto.Id).Customer.FirstName;
                    commentDto.CustomerLastName = commentList.First(s => s.Id == commentDto.Id).Customer.LastName;
                });
                return new SuccessDataResult<List<TripCommentDto>>("All comments of customer listed", commentDtoList);
            }
            return new ErrorDataResult<List<TripCommentDto>>("Comments of customer couldn't listed", null);
        }

        public async Task<DataResult<TripCommentDto>> UpdateComment(UpdateTripCommentDto comment, int commentId)
        {
            var entityComment = await commentRepository.GetByIdAsync(commentId);
            if (entityComment != null)
            {
                entityComment.Text = comment.Text ?? entityComment.Text;
                entityComment.Star = comment.Star == 0 ? entityComment.Star : comment.Star;
                await commentRepository.Update(entityComment);
                var commentDto = mapper.Map<TripCommentDto>(entityComment);
                return new SuccessDataResult<TripCommentDto>("Comment updated", commentDto);
            }
            return new ErrorDataResult<TripCommentDto>("Comment couldn't update", null);
        }
    }
}
