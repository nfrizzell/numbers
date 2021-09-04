using Microsoft.AspNetCore.Mvc;
using numbers.Models;

namespace numbers.Controllers
{
    public class FormsController : Controller
    {
        private readonly NumbersService numbersService;
        private const string FACT_PLACEHOLDER = "(n <= 10000)";
        private const string PRIME_PLACEHOLDER = "(e.g. 2147483647)";
        private const string PERM_PLACEHOLDER = "(n <= 8)";

        public FormsController(NumbersService numbersService)
        {
            this.numbersService = numbersService;
        }

        public IActionResult Prime(string input)
        {
            string previous = PRIME_PLACEHOLDER;

            if (!string.IsNullOrEmpty(input))
            {
                previous = input;
            }

            return View(model: new FormResult(previous, numbersService.CheckPrime(input)));
        }

        public IActionResult Factorial(string input)
        {
            string previous = FACT_PLACEHOLDER;

            if (!string.IsNullOrEmpty(input))
            {
                previous = input;
            }

            return View(model: new FormResult(previous, numbersService.CalculateFactorial(input)));
        }

        public IActionResult Regex()
        {
            
            return View();
        }
        public IActionResult Permutation(string input)
        {
            string previous = PERM_PLACEHOLDER;

            if (!string.IsNullOrEmpty(input))
            {
                previous = input;
            }

            return View(model: new FormResult(previous, numbersService.ListPermutations(input)));
        }
    }
}