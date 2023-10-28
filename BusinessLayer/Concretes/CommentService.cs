using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Comment;
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

        public List<CommentDto> GetAllCommentDtoList()
        {
            try
            {
                var commentDtoList = mapper.Map<List<CommentDto>>(GetAllCommentList().ToList());
                var commentCustomerIdList = commentDtoList.Select(s => s.CustomerId);
                var commentTripIdList = commentDtoList.Select(s => s.TripId);
                var customers = customerService.GetAllCustomerList().Where(s => commentCustomerIdList.Contains(s.Id));
                var trips = tripService.GetAllTripList().Where(s => commentTripIdList.Contains(s.Id));

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

                return commentDtoList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public IQueryable<Comment> GetAllCommentList()
        {
            return commentRepository.GetAll();
        }

        public CommentDto GetCommentDtoById(int id)
        {
            var comment = mapper.Map<Task<CommentDto>>(commentRepository.GetByIdAsync(id).Result).Result;
            var trip = tripService.GetTripById(comment.TripId).Result;
            var customer = customerService.GetCustomerById(comment.CustomerId).Result;
            comment.CustomerEmail = customer.Email;
            comment.CustomerFirstName = customer.FirstName;
            comment.CustomerLastName = customer.LastName;
            comment.TripName = trip.Title;
            comment.TripDate = trip.PlannedDate;
            return comment;
        }

        public List<CommentDto> GetCommentDtoListOfCustomer(int customerId)
        {
            var commentList = mapper.Map<List<CommentDto>>(commentRepository.GetWhere(c => c.CustomerId == customerId).ToList());
            return commentList;
        }
    }
}
