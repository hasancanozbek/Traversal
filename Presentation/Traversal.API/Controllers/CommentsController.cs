using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet("GetCommentDtoListOfCustomer")]
        public IActionResult GetCommentDtoListOfCustomer(int customerId) 
        {
            var result = commentService.GetCommentDtoListOfCustomer(customerId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllCommentDtoList")]
        public IActionResult GetAllCommentDtoList()
        {
            var result = commentService.GetAllCommentDtoList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCommentDtoById")]
        public async Task<IActionResult> GetCommentDtoById(int id)
        {
            var result = await commentService.GetCommentDtoById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(AddCommentDto comment)
        {
            var result = await commentService.AddComment(comment);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto comment, int commentId)
        {
            var result = await commentService.UpdateComment(comment, commentId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("DeleteComment")]
        public async Task<IActionResult> DeleteComment(CommentDto comment)
        {
            var result = await commentService.DeleteComment(comment);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
