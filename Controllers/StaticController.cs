using Microsoft.AspNetCore.Mvc;
using numbers.Models;

namespace numbers.Controllers
{
    public class StaticController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Regex()
		{
			return View();
		}

        public IActionResult Kattis()
		{
			return View();
		}
		public IActionResult Links()
		{
			return View();
		}

        public IActionResult About()
		{
			return View();
		}
    }
}