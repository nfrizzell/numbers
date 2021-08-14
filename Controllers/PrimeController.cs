using Microsoft.AspNetCore.Mvc;
using numbers.Models;

namespace numbers.Controllers
{
    public class PrimeController : Controller
    {
        private readonly NumbersDBContext dbContext;

        public PrimeController(NumbersDBContext context)
        {
            this.dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}