using Microsoft.AspNetCore.Mvc;
using numbers.Models;

namespace numbers.Controllers
{
    public class APIController : Controller
    {
        NumbersService numbersService;

        public APIController(NumbersService numbersService)
        {
            this.numbersService = numbersService;
        }

        [HttpGet]
        [Route("api/count/{increment}")]
        public ActionResult<ulong> Count(ulong increment)
        {
            string ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ulong result = numbersService.Count(increment, ip);
            return result;
        }
    }
}