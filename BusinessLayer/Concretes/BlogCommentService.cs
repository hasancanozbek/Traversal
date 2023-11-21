using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.BlogComments;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concretes
{
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IBlogCommentRepository commentRepository;
        private readonly IMapper mapper;

        public BlogCommentService(IBlogCommentRepository commentRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddComment(AddBlogCommentDto comment)
        {
            var commentEntity = mapper.Map<BlogComment>(comment);
            await commentRepository.AddAsync(commentEntity);
            return new SuccessResult("Comment added");
        }

        public async Task<Result> DeleteComment(BlogCommentDto comment)
        {
            var commentEntity = mapper.Map<BlogComment>(comment);
            await commentRepository.RemoveAsync(commentEntity);
            return new SuccessResult("Comment deleted");
        }

        public DataResult<List<BlogCommentDto>> GetAllCommentList()
        {
            try
            {
                var result = GetAllCommentsAsQueryable();
                if (result.IsSuccess)
                {
                    var commentList = result.Data.Include(i => i.Customer);
                    var commentDtoList = mapper.Map<List<BlogCommentDto>>(commentList);
                    commentDtoList.ForEach(commentDto =>
                    {
                        var comment = commentList.First(s => s.Id == commentDto.Id);
                        commentDto.CustomerFullName = comment.Customer.FirstName + " " + comment.Customer.LastName;
                    });
                    return new SuccessDataResult<List<BlogCommentDto>>(commentDtoList);
                }
                return new ErrorDataResult<List<BlogCommentDto>>(null);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<BlogCommentDto>>(exception.Message, null);
            }
        }

        public DataResult<IQueryable<BlogComment>> GetAllCommentsAsQueryable()
        {
            var commentList = commentRepository.GetAll();
            return new SuccessDataResult<IQueryable<BlogComment>>("All comments listed", commentList);
        }

        public async Task<DataResult<BlogCommentDto>> GetCommentById(int id)
        {
            var comment = await commentRepository.GetWhere(s => s.Id == id).Include(i => i.Customer).FirstOrDefaultAsync();
            var commentDto = mapper.Map<BlogCommentDto>(comment);
            commentDto.CustomerFullName = comment.Customer.FirstName + " " + comment.Customer.LastName;
            return new SuccessDataResult<BlogCommentDto>("Comment information listed", commentDto);
        }

        public DataResult<List<BlogCommentDto>> GetCommentListOfBlogById(int blogId)
        {
            var commentList = commentRepository.GetWhere(c => c.BlogId == blogId).Include(i => i.Customer);
            var commentDtoList = mapper.Map<List<BlogCommentDto>>(commentList);
            commentDtoList.ForEach(commentDto =>
            {
                var comment = commentList.First(s => s.Id == commentDto.Id);
                commentDto.CustomerFullName = comment.Customer.FirstName + " " + comment.Customer.LastName;
            });
            return new SuccessDataResult<List<BlogCommentDto>>("All comments of blog listed", commentDtoList);
        }

        public DataResult<List<BlogCommentDto>> GetCommentListOfCustomerById(int customerId)
        {
            var commentList = commentRepository.GetWhere(c => c.CustomerId == customerId).Include(i => i.Customer);
            var commentDtoList = mapper.Map<List<BlogCommentDto>>(commentList);
            commentDtoList.ForEach(commentDto =>
            {
                var comment = commentList.First(s => s.Id == commentDto.Id);
                commentDto.CustomerFullName = comment.Customer.FirstName + " " + comment.Customer.LastName;
            });
            return new SuccessDataResult<List<BlogCommentDto>>("All comments of blog listed", commentDtoList);
        }

        public async Task<DataResult<BlogCommentDto>> UpdateComment(UpdateBlogCommentDto comment, int commentId)
        {
            var entityComment = await commentRepository.GetByIdAsync(commentId);
            if (entityComment != null)
            {
                entityComment.Text = comment.Text ?? entityComment.Text;
                await commentRepository.Update(entityComment);
                var commentDto = mapper.Map<BlogCommentDto>(entityComment);
                return new SuccessDataResult<BlogCommentDto>("Comment updated", commentDto);
            }
            return new ErrorDataResult<BlogCommentDto>("Comment couldn't update", null);
        }
    }
}
