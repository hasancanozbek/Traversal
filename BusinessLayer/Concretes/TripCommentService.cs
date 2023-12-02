using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.BlogComments;
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
        private readonly ICustomerService customerService;
        private readonly ITripDateService tripService;
        private readonly IMapper mapper;

        public TripCommentService(ITripCommentRepository commentRepository, IMapper mapper, ICustomerService customerService, ITripDateService tripService)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
            this.customerService = customerService;
            this.tripService = tripService;
        }

        public async Task<Result> AddComment(AddTripCommentDto comment)
        {
            var commentEntity = mapper.Map<TripComment>(comment);
            await commentRepository.AddAsync(commentEntity);
            return new SuccessResult("Comment added");
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
            return new SuccessDataResult<IQueryable<TripComment>>("All comments listed" , commentList);
        }

        public async Task<DataResult<TripCommentDto>> GetCommentById(int id)
        {
            var comment = await commentRepository.GetWhere(s => s.Id == id).Include(i => i.Customer).Include(i => i.TripDate.Trip).FirstOrDefaultAsync();
            var commentDto = mapper.Map<TripCommentDto>(comment);
            commentDto.CustomerFirstName = comment.Customer.FirstName;
            commentDto.CustomerLastName = comment.Customer.LastName;
            commentDto.CustomerEmail = comment.Customer.Email;
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
            var commentList = mapper.Map<List<TripCommentDto>>(commentRepository.GetWhere(c => c.TripDateId == tripId).ToList());
            return new SuccessDataResult<List<TripCommentDto>>("All comments of customer listed", commentList);
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
