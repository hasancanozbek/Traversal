using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Blogs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Traversal.Web.Models;

namespace Traversal.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IBlogCommentService blogCommentService;
        private readonly ICustomerService customerService;
        public BlogsController(IBlogService blogService, IBlogCommentService blogCommentService, ICustomerService customerService)
        {
            this.blogService = blogService;
            this.blogCommentService = blogCommentService;
            this.customerService = customerService;
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
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    ViewBag.UserId = userId;
                }
                var model = new BlogDetailViewModel()
                {
                    Blog = result.Data
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult BlogDetail(AddBlogDto blogDto)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(BlogDetailViewModel model)
        {
            var customerResult = customerService.GetCustomerByUserId(model.AddBlogCommentDto.CustomerId);
            if (customerResult.IsSuccess)
            {
                model.AddBlogCommentDto.CustomerId = customerResult.Data.Id;
                var result = await blogCommentService.AddComment(model.AddBlogCommentDto);
                if (result.IsSuccess)
                {
                    return RedirectToAction("BlogDetail", new { id = model.AddBlogCommentDto.BlogId });
                }
            };
            return View("/Blogss/Index");
        }
    }
}
