using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Blogs;
using Microsoft.AspNetCore.Mvc;
using Traversal.Web.Areas.Admin.Models;

namespace Traversal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AdminBlogsController : Controller
    {
        private readonly IBlogService blogService;

        public AdminBlogsController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var blogList = blogService.GetAllBlogList(includePassives: true).Data;
            return View(blogList);
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddBlogDto blogDto)
        {
            if (blogDto != null)
            {
                await blogService.AddBlog(blogDto);
            }
            return RedirectToAction("Index", "AdminBlogs");
        }

        [HttpGet("Update/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var model = new AdminUpdateBlogModel();
            var blog = await blogService.GetBlogById(Id);
            if (blog.Data != null)
            {
                model.CurrentBlog = blog.Data;
            }
            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(AdminUpdateBlogModel model)
        {
            if (model != null)
            {
                await blogService.UpdateBlog(model.UpdateBlog, model.CurrentBlog.Id);
            }
            return RedirectToAction("Index", "AdminBlogs");
        }

        [HttpGet("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var blog = blogService.GetAllBlogsAsQueryable(true).Data.Where(s => s.Id == Id).ToList().FirstOrDefault();
            if (blog != null)
            {
                await blogService.SetActive(blog, false);
            }
            return RedirectToAction("Index", "AdminBlogs");
        }
    }
}
