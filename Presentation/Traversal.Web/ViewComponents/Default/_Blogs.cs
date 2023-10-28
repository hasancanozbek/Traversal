using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.ViewComponents.Default
{
    public class _Blogs : ViewComponent
    {
        private readonly IBlogService blogService;

        public _Blogs(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            //var blogList = blogService.GetAllBlogList().OrderByDescending(o => o.CreatedTime);
            //return View(blogList);
            return View();
        }
    }
}