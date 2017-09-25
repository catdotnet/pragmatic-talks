using Microsoft.AspNetCore.Mvc;

namespace PragmaticTalks.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string errorMessage = null)
        {
            if (!string.IsNullOrEmpty(errorMessage))
                ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
