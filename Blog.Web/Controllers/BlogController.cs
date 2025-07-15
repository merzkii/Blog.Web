using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
