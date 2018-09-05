using Microsoft.AspNetCore.Mvc;

namespace TestFullStack.WebApi.Controllers
{

    /**
     * Controller use to test WebApi is running
     **/

    [Route("api/[controller]")]
    [ApiController]
    public class InitController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Aplication ON";
        }
    }
}
