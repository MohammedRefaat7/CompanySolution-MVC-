using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
