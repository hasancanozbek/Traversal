using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.ViewComponents.Default
{
    public class _Testimonial : ViewComponent
    {
        private readonly ITripCommentService commentService;

        public _Testimonial(ITripCommentService commentService)
        {
            this.commentService = commentService;
        }

        public IViewComponentResult Invoke()
        {
            var result = commentService.GetAllCommentList();
            if (result.IsSuccess)
            {
                var commentList = result.Data.Take(5).OrderByDescending(o => o.CreatedTime).ToList();
                return View(commentList);
            }
            return View();
        }
    }
}