using Data.DTOs.Section;
using Microsoft.AspNetCore.Mvc;
using Service.BaseResponses;
using Service.BookServices;
using Service.SectionServices;
using Service.ShelfServices;
using Service.User;

namespace Library.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly IBookService _bookService;
        private readonly IShelfService _shelfService;
        private readonly IAppUserService _userService;
        public SectionController(ISectionService sectionService, IBookService bookService, IShelfService shelfService, IAppUserService userService)
        {
            _sectionService = sectionService;
            _bookService = bookService;
            _shelfService = shelfService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            ViewBag.dataSource = await _sectionService.GetSections();
            ViewBag.Books = await _bookService.GetBooks();
            ViewBag.Shelves = await _shelfService.GetShelves();
            return View();
        }
        public async Task<IActionResult> GetSection(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _sectionService.GetSection(id);
            return Json(result);
        }
        public async Task<IActionResult> ManageSection(SectionDTO sectionDTO)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = new ResponseResult();
            if (sectionDTO.Id > 0)
            {
                result = await _sectionService.UpdateSection(sectionDTO);
            }
            else
            {
                result = await _sectionService.AddSection(sectionDTO);
            }
            return Json(result);
        }
        public async Task<IActionResult> DeleteSection(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _sectionService.DeleteSection(id);
            return Json(result);
        }

    }
}
