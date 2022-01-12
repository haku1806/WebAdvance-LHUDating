using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HenHoCore.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;

        public HomeController(IWebHostEnvironment hostEnvironment)
        {
            this._hostEnvironment = hostEnvironment;
        }

        [Route("home")]
        public string Get()
        {
            return "Api ready!";
        }
    }
}
