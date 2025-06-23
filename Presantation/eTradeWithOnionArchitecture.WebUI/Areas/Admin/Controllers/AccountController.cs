using Microsoft.AspNetCore.Mvc;

namespace eTradeWithOnionArchitecture.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        } 
        public IActionResult Register()
        {
            return View();
        }


    }
}
