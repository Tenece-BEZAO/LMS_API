using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    public class AuthenticationContoller : Controller
    {
       // [Swagger]
        public IActionResult Index()
        {
            return View();
        }
    }
}
