using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.ViewComponents.Default
{
    public class _Testimonial : ViewComponent
    {
        private readonly ICommentService commentService;

        public _Testimonial(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public IViewComponentResult Invoke()
        {
            var commentList = commentService.GetAllCommentDtoList().Take(5).OrderByDescending(o => o.CreatedTime).ToList();
            return View(commentList);
        }
    }
}