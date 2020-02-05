using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.Mvc.Controllers
{
	public class BaseDotnetDeskController : Controller
    {
        protected bool IsHaveEnoughAccessRight()
        {
            //todo: cek controller
            //todo: cek user
            return true;
        }

    }
}