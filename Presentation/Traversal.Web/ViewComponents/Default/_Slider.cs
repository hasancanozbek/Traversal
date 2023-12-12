using BusinessLayer.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Web.ViewComponents.Default
{
    public class _Slider : ViewComponent
    {
        private readonly ICountryService countryService;

        public _Slider(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        public IViewComponentResult Invoke()
        {
            var result = countryService.GetAllCountryList();
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
