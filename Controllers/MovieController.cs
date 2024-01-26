using Microsoft.AspNetCore.Mvc;

namespace YourNamespace
{
    public class MovieController : Controller
    {
        // GET: /Movies/
        public IActionResult Index()
        {
            return View();
        }


    }
}
