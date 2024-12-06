using Microsoft.AspNetCore.Mvc;

namespace GastroLab.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
