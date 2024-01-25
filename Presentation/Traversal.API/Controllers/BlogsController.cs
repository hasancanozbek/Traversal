using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService blogService;
        private readonly IMapper mapper;
        public BlogsController(IBlogService blogService, IMapper mapper)
        {
            this.blogService = blogService;
            this.mapper = mapper;
        }

        [HttpGet("GetAllBlogList")]
        public IActionResult GetAllBlogList()
        {
            var result = blogService.GetAllBlogList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetBlogById")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var result = await blogService.GetBlogById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddBlog")]
        public async Task<IActionResult> AddTripLocation(AddBlogDto blogDto)
        {
            var result = await blogService.AddBlog(blogDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto blogDto, int blogId)
        {
            var result = await blogService.UpdateBlog(blogDto, blogId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteBlog")]
        public async Task<IActionResult> DeleteBlog(BlogDto blogDto)
        {
            var result = await blogService.DeleteBlog(blogDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
