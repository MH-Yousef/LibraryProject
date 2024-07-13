using Data.DTOs.Shlef;
using Microsoft.AspNetCore.Mvc;
using Service.BookServices;
using Service.CategoryServices;
using Service.SectionServices;
using Service.ShelfServices;
using Service.User;
using Syncfusion.EJ2.Schedule;

namespace Library.Controllers
{
    public class ShelfController : Controller
    {
        private readonly IShelfService _shelfService;
        private readonly IBookService _bookService;
        private readonly ISectionService _sectionService;
        private readonly ICategoryService _categoryService;
        private readonly IAppUserService _userService;
        public ShelfController(IShelfService shelfService, IBookService bookService, ISectionService sectionService, ICategoryService categoryService, IAppUserService userService)
        {
            _shelfService = shelfService;
            _bookService = bookService;
            _sectionService = sectionService;
            _categoryService = categoryService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            ViewBag.dataSource = await _shelfService.GetShelves();
            ViewBag.Books = await _bookService.GetBooks();
            ViewBag.Sections = await _sectionService.GetSections();
            ViewBag.Categories = await _categoryService.GetCategories();
            return View();
        }
        [HttpGet]

        #region ShelfDetails
        public async Task<IActionResult> GetShelf(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _shelfService.GetShelf(id);
            return Json(result);
        }
        #endregion

        #region ManageShelf
        public async Task<IActionResult> ManageShelf(ShelfDTO shelfDTO)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            if (shelfDTO.Id == 0)
            {
                var result = await _shelfService.AddShelf(shelfDTO);
                return Json(result);
            }
            else
            {
                var result = await _shelfService.UpdateShelf(shelfDTO);
                return Json(result);
            }
        }
        #endregion

        #region DeleteShelf
        public async Task<IActionResult> UpdateShelf(ShelfDTO shelfDTO)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _shelfService.UpdateShelf(shelfDTO);
            return Json(result);
        }
        #endregion

        #region DeleteShelf
        public async Task<IActionResult> DeleteShelf(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _shelfService.DeleteShelf(id);
            return Json(result);
        }
        #endregion
    }
}
