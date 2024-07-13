using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.User;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public HomeController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
