using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Blogs;
using Core.Enums;
using Core.Utilities.Cloud;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concretes
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository blogRepository;
        private readonly IBlogKeyRepository blogKeyRepository;
        private readonly IBlogCommentService blogCommentService;
        private readonly ICloudRepo cloudRepo;
        private readonly IMapper mapper;

		public BlogService(IBlogRepository blogRepository, IMapper mapper, IBlogKeyRepository blogKeyRepository, ICloudRepo cloudRepo, IBlogCommentService blogCommentService)
		{
			this.blogRepository = blogRepository;
			this.mapper = mapper;
			this.blogKeyRepository = blogKeyRepository;
			this.cloudRepo = cloudRepo;
			this.blogCommentService = blogCommentService;
		}

		public async Task<Result> AddBlog(AddBlogDto blog)
        {
            var blogEntity = mapper.Map<Blog>(blog);
            var entityId = await blogRepository.AddAsync(blogEntity);
            foreach (var image in blog.ImageList)
            {
                var fileAssetId = await cloudRepo.UploadFileAsync(@image, FileTypesEnum.Image);
                if (!fileAssetId.Equals(string.Empty))
                {
                    var keyValuePair = new BlogKey()
                    {
                        BlogId = entityId,
                        Key = BlogKeysEnum.image.ToString(),
                        Value = fileAssetId
                    };
                    await blogKeyRepository.AddAsync(keyValuePair);
                }
            }
            return new SuccessResult("Blog added");
        }

        public async Task<Result> DeleteBlog(BlogDto blog)
        {
            var blogEntity = mapper.Map<Blog>(blog);
            blogCommentService.DeleteAllCommentOfBlog(blog.Id);
            await blogRepository.RemoveAsync(blogEntity);
            var imageList = blogKeyRepository.GetWhere(s => s.BlogId == blog.Id).ToList();
            await blogKeyRepository.RemoveRange(imageList);
            return new SuccessResult("Blog deleted");
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
				blog.Comments = blogCommentService.GetCommentListOfBlogById(blog.Id).Data;
				blog.ImageList = new List<string>();
                var imageKeys = blogKeyRepository.GetWhere(s => s.BlogId == blog.Id && s.Key == BlogKeysEnum.image.ToString()).OrderBy(o => o.CreatedTime).Select(s => s.Value).ToList();
                foreach (var image in imageKeys)
                {
                    var url = cloudRepo.GetFileUrl(image);
                    blog.ImageList.Add(url);
                }
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

            blogDto.Comments = blogCommentService.GetCommentListOfBlogById(blogId).Data;

            blogDto.ImageList = new List<string>();
            var imageKeys = blogKeyRepository.GetWhere(s => s.BlogId == blogId && s.Key == BlogKeysEnum.image.ToString()).OrderBy(o => o.CreatedTime).Select(s => s.Value).ToList();
            foreach (var image in imageKeys)
            {
                var url = await cloudRepo.GetFileUrlAsync(image);
                blogDto.ImageList.Add(url);
            }

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
					blog.Comments = blogCommentService.GetCommentListOfBlogById(blog.Id).Data;

					blog.ImageList = new List<string>();
                    var imageKeys = blogKeyRepository.GetWhere(s => s.BlogId == blog.Id && s.Key == BlogKeysEnum.image.ToString()).OrderBy(o => o.CreatedTime).Select(s => s.Value).ToList();
                    foreach (var image in imageKeys)
                    {
                        var url = cloudRepo.GetFileUrl(image);
                        blog.ImageList.Add(url);
                    }

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
                await blogRepository.Update(blogEntity);
                var mappedBlog = mapper.Map<BlogDto>(blogEntity);
                return new SuccessDataResult<BlogDto>("Blog updated", mappedBlog);
            }
            return new ErrorDataResult<BlogDto>("Blog couldn't update", null);
        }
    }
}
