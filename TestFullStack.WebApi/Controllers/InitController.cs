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
        /// <summary>
        /// Use to verify run app - only developer mode
        /// </summary>
        /// <returns>System start informative text</returns>
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Aplication ON";
        }
    }
}
