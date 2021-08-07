using Microsoft.AspNetCore.Mvc;
using numbers.Models;
using numbers.Services;

namespace numbers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : ControllerBase
    {
        public NumberController()
        {
        }

        [HttpGet("{num}")]
        public ActionResult<Number> Get(int num)
        {
            return NumberService.Get(num);
        }
    }
}