using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Guides;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidesController : ControllerBase
    {
        private readonly IGuideService guideService;

        public GuidesController(IGuideService guideService)
        {
            this.guideService = guideService;
        }

        [HttpGet("GetAllGuideList")]
        public IActionResult GetAllGuideList()
        {
            var result = guideService.GetAllGuideList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetGuideById")]
        public async Task<IActionResult> GetGuideById(int guideId)
        {
            var result = await guideService.GetGuideById(guideId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddGuide")]
        public async Task<IActionResult> AddGuide(AddGuideDto guide)
        {
            var result = await guideService.AddGuide(guide);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateGuide")]
        public async Task<IActionResult> UpdateGuide(UpdateGuideDto guide, int guideId)
        {
            var result = await guideService.UpdateGuide(guide, guideId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteGuide")]
        public async Task<IActionResult> DeleteGuide(GuideDto guide)
        {
            var result = await guideService.DeleteGuide(guide);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
