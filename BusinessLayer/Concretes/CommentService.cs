using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Comments;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly ICustomerService customerService;
        private readonly ITripService tripService;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, ICustomerService customerService, ITripService tripService)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
            this.customerService = customerService;
            this.tripService = tripService;
        }

        public async Task<Result> AddComment(AddCommentDto comment)
        {
            var commentEntity = mapper.Map<Comment>(comment);
            await commentRepository.AddAsync(commentEntity);
            return new SuccessResult("Comment added");
        }

        public async Task<Result> DeleteComment(CommentDto comment)
        {
            var commentEntity = mapper.Map<Comment>(comment);
            await commentRepository.RemoveAsync(commentEntity);
            return new SuccessResult("Comment deleted");
        }

        public DataResult<List<CommentDto>> GetAllCommentDtoList()
        {
            try
            {
                var commentDtoList = mapper.Map<List<CommentDto>>(GetAllCommentList().Data.ToList());
                var commentCustomerIdList = commentDtoList.Select(s => s.CustomerId);
                var commentTripIdList = commentDtoList.Select(s => s.TripId);
                var customers = customerService.GetAllCustomerList().Data.Where(s => commentCustomerIdList.Contains(s.Id));
                var trips = tripService.GetAllTripList().Data.Where(s => commentTripIdList.Contains(s.Id));

                foreach (var comment in commentDtoList)
                {
                    var customerInfos = customers.FirstOrDefault(s => s.Id == comment.CustomerId);
                    var tripInfos = trips.FirstOrDefault(s => s.Id == comment.TripId);
                    if (customerInfos != null && tripInfos != null)
                    {
                        comment.CustomerFirstName = customerInfos.FirstName;
                        comment.CustomerLastName = customerInfos.LastName;
                        comment.CustomerEmail = customerInfos.Email;
                        comment.TripName = tripInfos.Title;
                    }
                }
                return new SuccessDataResult<List<CommentDto>>(commentDtoList);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<CommentDto>>(null);
            }
            
        }

        public DataResult<IQueryable<Comment>> GetAllCommentList()
        {
            var commentList = commentRepository.GetAll();
            return new SuccessDataResult<IQueryable<Comment>>("All comments listed" , commentList);
        }

        public async Task<DataResult<CommentDto>> GetCommentDtoById(int id)
        {
            var comment = await mapper.Map<Task<CommentDto>>(commentRepository.GetByIdAsync(id));
            var trip = await tripService.GetTripById(comment.TripId);
            var customer = await customerService.GetCustomerById(comment.CustomerId);
            comment.CustomerEmail = customer.Data.Email;
            comment.CustomerFirstName = customer.Data.FirstName;
            comment.CustomerLastName = customer.Data.LastName;
            comment.TripName = trip.Data.Title;
            comment.TripDate = trip.Data.PlannedDate;
            return new SuccessDataResult<CommentDto>("Comment information listed", comment);
        }

        public DataResult<List<CommentDto>> GetCommentDtoListOfCustomer(int customerId)
        {
            var commentList = mapper.Map<List<CommentDto>>(commentRepository.GetWhere(c => c.CustomerId == customerId).ToList());
            return new SuccessDataResult<List<CommentDto>>("All comments of customer listed", commentList);
        }

        public async Task<DataResult<CommentDto>> UpdateComment(UpdateCommentDto comment, int commentId)
        {
            var entityComment = await commentRepository.GetByIdAsync(commentId);
            if (entityComment != null)
            {
                entityComment.Text = comment.Text ?? entityComment.Text;
                entityComment.Star = comment.Star == 0 ? entityComment.Star : comment.Star;
                await commentRepository.Update(entityComment);
                var commentDto = mapper.Map<CommentDto>(entityComment);
                return new SuccessDataResult<CommentDto>("Comment updated", commentDto);
            }
            return new ErrorDataResult<CommentDto>("Comment couldn't update", null);
        }
    }
}
