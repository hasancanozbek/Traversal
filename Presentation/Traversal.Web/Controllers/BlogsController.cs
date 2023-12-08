using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Blogs;
using Core.Utilities.Cloud;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService blogService;
        public BlogsController(IBlogService blogService)
        {
            this.blogService = blogService;        
        }

        public IActionResult Index()
        {
            var result = blogService.GetAllBlogList();
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BlogDetail(int id)
        {
            var result = await blogService.GetBlogById(id);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }

        [HttpPost]
        public IActionResult BlogDetail(AddBlogDto blogDto)
        {
            return View();
        }

    }
}
