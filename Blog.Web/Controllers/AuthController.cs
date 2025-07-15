using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
