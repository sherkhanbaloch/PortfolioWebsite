using Microsoft.AspNetCore.Mvc;

namespace PortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
