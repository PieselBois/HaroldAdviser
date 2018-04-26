using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaroldAdviser.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties() {RedirectUri = returnUrl});
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(".AspNetCore.Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}