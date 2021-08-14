using Microsoft.AspNetCore.Mvc;
using numbers.Models;

namespace numbers.Controllers
{
    public class FormsController : Controller
    {
        private readonly NumbersDBContext dbContext;

        public FormsController(NumbersDBContext context)
        {
            this.dbContext = context;
        }

        public IActionResult Prime()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prime(string input)
        {
            return View(dbContext.CheckNumberPrime(input));
        }

        public IActionResult Factorial()
        {
            return View();
        }
    }
}