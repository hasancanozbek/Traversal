using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Comments;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripCommentsController : ControllerBase
    {
        private readonly ITripCommentService commentService;

        public TripCommentsController(ITripCommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet("GetCommentListOfCustomerById")]
        public IActionResult GetCommentListOfCustomerById(int customerId) 
        {
            var result = commentService.GetCommentListOfCustomerById(customerId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllCommentList")]
        public IActionResult GetAllCommentList()
        {
            var result = commentService.GetAllCommentList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCommentById")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var result = await commentService.GetCommentById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(AddTripCommentDto comment)
        {
            var result = await commentService.AddComment(comment);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(UpdateTripCommentDto comment, int commentId)
        {
            var result = await commentService.UpdateComment(comment, commentId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(TripCommentDto comment)
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
