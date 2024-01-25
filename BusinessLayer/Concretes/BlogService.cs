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
            if (blog.ImageList != null)
            {
                foreach (var image in blog.ImageList)
                {
                    var fileAssetId = await cloudRepo.UploadFileAsync(image, FileTypesEnum.Image);
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

        public DataResult<List<BlogDto>> GetAllBlogList(bool includePassives = false)
        {
            var blogs = blogRepository.GetAll();
            if (!includePassives)
            {
                blogs = blogs.Where(s => s.IsActive);
            }
            var blogList = blogs.ToList();
            var blogListDto = mapper.Map<List<BlogDto>>(blogList);
            blogListDto.ForEach(blog =>
            {
                var tmpBlog = blogList.First(s => s.Id == blog.Id);
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

        public DataResult<IQueryable<Blog>> GetAllBlogsAsQueryable(bool tracking = false)
        {
            var blogList = blogRepository.GetAll(tracking);
            return new SuccessDataResult<IQueryable<Blog>>(blogList);
        }

        public async Task<DataResult<BlogDto>> GetBlogById(int blogId)
        {
            var blog = await blogRepository.GetWhere(s => s.Id == blogId).FirstOrDefaultAsync();
            var blogDto = mapper.Map<BlogDto>(blog);

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

        public async Task SetActive(Blog entity, bool isActive)
        {
            if (entity != null)
            {
                await blogRepository.SetActivity(entity, isActive);
            }
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
