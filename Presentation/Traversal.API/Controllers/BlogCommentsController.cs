using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.BlogComments;
using BusinessLayer.Dtos.Comments;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentsController : ControllerBase
    {
        private readonly IBlogCommentService blogCommentService;

        public BlogCommentsController(IBlogCommentService blogCommentService)
        {
            this.blogCommentService = blogCommentService;
        }

        [HttpGet("GetCommentListOfCustomerById")]
        public IActionResult GetCommentListOfCustomerById(int customerId)
        {
            var result = blogCommentService.GetCommentListOfCustomerById(customerId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCommentListOfBlogById")]
        public IActionResult GetCommentListOfBlogById(int blogId)
        {
            var result = blogCommentService.GetCommentListOfBlogById(blogId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllCommentList")]
        public IActionResult GetAllCommentList()
        {
            var result = blogCommentService.GetAllCommentList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCommentById")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var result = await blogCommentService.GetCommentById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(AddBlogCommentDto comment)
        {
            var result = await blogCommentService.AddComment(comment);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(UpdateBlogCommentDto comment, int commentId)
        {
            var result = await blogCommentService.UpdateComment(comment, commentId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(BlogCommentDto comment)
        {
            var result = await blogCommentService.DeleteComment(comment);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
