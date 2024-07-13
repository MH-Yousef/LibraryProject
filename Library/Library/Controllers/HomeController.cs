using Microsoft.AspNetCore.Mvc;
using Service.BookServices;
using Service.FrenidShipServices;
using Service.User;
using System.Diagnostics;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUserService UserService;
        private readonly IBookService BookService;
        private readonly IFreindShipService _freindShipService;
        public HomeController(ILogger<HomeController> logger, IAppUserService appUserService, IBookService bookService, IFreindShipService freindShipService)
        {
            _logger = logger;
            UserService = appUserService;
            BookService = bookService;
            _freindShipService = freindShipService;
        }

        public async Task<IActionResult>  Index()
        {
            if (UserService.IsSignIn())
            {
                var user = await UserService.GetUser();
                var id = user.Id;
                var requests = await _freindShipService.GetFreindRequestsReceive(id);
                var sentRequests = await _freindShipService.GetFreindRequestsSent(id);
                var Friends = await _freindShipService.GetFreinds(id);

                ViewBag.Friends = Friends;
                ViewBag.sentRequests = sentRequests;
                ViewBag.requests = requests;
                ViewBag.CurrentUser = user;
                ViewBag.Name = user.FullName;
                ViewBag.Users = await UserService.GetAddFreindList(id);
                var Books = await BookService.GetBooks();
                return View(Books);
            }
            return RedirectToAction("Login", "Account");
        }

    }
}
