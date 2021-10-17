using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHUDatingCore.Controllers
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
