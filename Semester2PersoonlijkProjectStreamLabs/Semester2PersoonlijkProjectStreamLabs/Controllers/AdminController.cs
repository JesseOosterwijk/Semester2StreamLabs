using Microsoft.AspNetCore.Mvc;

namespace Semester2PersoonlijkProjectStreamLabs.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}