using Microsoft.AspNetCore.Mvc;
using numbers.Models;

namespace numbers.Controllers
{
    public class KattisController : Controller
    {
        private ProblemService problemService;

        public KattisController(ProblemService problemService)
        {
            this.problemService = problemService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Problem(string id)
        {
            return View(model: problemService.Load(id));
        }
    }
}