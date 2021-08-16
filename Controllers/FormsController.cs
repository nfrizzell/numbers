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

        public IActionResult Prime(string input="")
        {
            return View(model:dbContext.CheckNumberPrime(input));
        }

        public IActionResult Factorial(string input="")
        {
            return View(model:dbContext.RetrieveFactorial(input));
        }

        public IActionResult Regex()
        {
            return View();
        }
    }
}