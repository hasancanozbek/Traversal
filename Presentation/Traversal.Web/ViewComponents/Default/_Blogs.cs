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
            var result = blogService.GetAllBlogList();
            if (result.IsSuccess)
            {
                return View(result.Data.OrderByDescending(o => o.CreatedTime));
            }
            return View();
        }
    }
}