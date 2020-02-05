using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.Mvc.Controllers
{
	public class AdminlteController : Controller
    {
        public IActionResult Blank()
        {
            return View();
        }
    }
}