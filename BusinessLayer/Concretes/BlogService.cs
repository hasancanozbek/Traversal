using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Blogs;
using BusinessLayer.Dtos.Trips;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using DataAccessLayer.Concretes;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concretes
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository blogRepository;
        private readonly IMapper mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddBlog(AddBlogDto blog)
        {
            var blogEntity = mapper.Map<Blog>(blog);
            await blogRepository.AddAsync(blogEntity);
            return new SuccessResult("Blog added");
        }

        public async Task<Result> DeleteBlog(BlogDto blog)
        {
            var blogEntity = mapper.Map<Blog>(blog);
            await blogRepository.RemoveAsync(blogEntity);
            return new SuccessResult("blog deleted");
        }

        public DataResult<List<BlogDto>> GetAllBlogList()
        {
            var blogList = blogRepository.GetAll().Include(i => i.Customer).ToList();
            var blogListDto = mapper.Map<List<BlogDto>>(blogList);
            blogListDto.ForEach(blog => 
            {
                var tmpBlog = blogList.First(s => s.Id == blog.Id);
                blog.CustomerFirstName = tmpBlog.Customer.FirstName;
                blog.CustomerLastName = tmpBlog.Customer.LastName;
            });
            return new SuccessDataResult<List<BlogDto>>(blogListDto);
        }

        public DataResult<IQueryable<Blog>> GetAllBlogsAsQueryable()
        {
            var blogList = blogRepository.GetAll();
            return new SuccessDataResult<IQueryable<Blog>>(blogList);
        }

        public async Task<DataResult<BlogDto>> GetBlogById(int blogId)
        {
            var blog = await blogRepository.GetWhere(s => s.Id == blogId).Include(i => i.Customer).FirstOrDefaultAsync();
            var blogDto = mapper.Map<BlogDto>(blog);
            blogDto.CustomerFirstName = blog.Customer.FirstName;
            blogDto.CustomerLastName = blog.Customer.LastName;
            return new SuccessDataResult<BlogDto>("Blog information listed", blogDto);
        }

        public DataResult<List<BlogDto>> GetBlogListByCustomerId(int customerId)
        {
            var blogList = blogRepository.GetWhere(s => s.CustomerId == customerId).Include(i => i.Customer).ToList();
            if (blogList.Any())
            {
                var customerFirstName = blogList.First().Customer.FirstName;
                var customerLastName = blogList.First().Customer.LastName;
                var blogListDto = mapper.Map<List<BlogDto>>(blogList);
                blogListDto.ForEach(blog =>
                {
                    blog.CustomerFirstName = customerFirstName;
                    blog.CustomerLastName = customerLastName;
                });
                return new SuccessDataResult<List<BlogDto>>(blogListDto);
            }
            return new ErrorDataResult<List<BlogDto>>("No blogs belonging to the customer were found", null);
        }

        public async Task<DataResult<BlogDto>> UpdateBlog(UpdateBlogDto blog, int blogId)
        {
            var blogEntity = await blogRepository.GetByIdAsync(blogId);
            if (blogEntity != null)
            {
                blogEntity.Title = blog.Title ?? blogEntity.Title;
                blogEntity.Content = blog.Content ?? blogEntity.Content;
                blogEntity.ImageList = blog.ImageList ?? blogEntity.ImageList;
                await blogRepository.Update(blogEntity);
                var mappedBlog = mapper.Map<BlogDto>(blogEntity);
                return new SuccessDataResult<BlogDto>("Blog updated", mappedBlog);
            }
            return new ErrorDataResult<BlogDto>("Blog couldn't update", null);
        }
    }
}
