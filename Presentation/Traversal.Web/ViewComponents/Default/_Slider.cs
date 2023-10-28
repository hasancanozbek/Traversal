using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.ViewComponents.Default
{
    public class _Slider : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
