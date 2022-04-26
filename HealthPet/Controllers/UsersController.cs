using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthPet.Controllers
{
    public class UsersController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
    }
}
