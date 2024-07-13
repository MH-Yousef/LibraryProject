using Core.Enums;
using Data.DTOs.Book;
using Microsoft.AspNetCore.Mvc;
using Service.BookServices;
using Service.CategoryServices;
using Service.ReviewServices;
using Service.SectionServices;
using Service.ShelfServices;
using Service.User;
using Syncfusion.EJ2.Linq;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly ISectionService _sectionService;
        private readonly IShelfService _shelfService;
        private readonly IReviewService _reviewService;
        private readonly IAppUserService _userService;
        public BookController(ICategoryService categoryService, IBookService bookService, ISectionService sectionService, IShelfService shelfService, IReviewService reviewService, IAppUserService userService)
        {
            _categoryService = categoryService;
            _bookService = bookService;
            _sectionService = sectionService;
            _shelfService = shelfService;
            _reviewService = reviewService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var IsSgined = _userService.IsSignIn();
            if(!IsSgined) return RedirectToAction("Login", "Account");

            ViewBag.Sections = await _sectionService.GetSections();
            ViewBag.Shelves = await _shelfService.GetShelves();
            ViewBag.Categories = await _categoryService.GetCategories();
            ViewBag.Books = await _bookService.GetBooks();
            return View();
        }

        #region BookDetails
        public async Task<IActionResult> BookDetails(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var book = await _bookService.GetBook(id);
            var category = await _categoryService.GetCategory(book.CategoryId);
            var section = await _sectionService.GetSection(book.SectionId);
            var shelf = await _shelfService.GetShelf(book.ShelfId);
            var ShareTypes = Enum.GetValues(typeof(ReviewShareType)).Cast<ReviewShareType>().ToList();
            ViewBag.User = await _userService.GetUser();
            ViewBag.CurrentCategory = category.Name;
            ViewBag.CurrentSection = section.Name;
            ViewBag.CurrentShelf = shelf.ShelfNumber;
            ViewBag.ShareTypes = ShareTypes;
            ViewBag.Reviews = await _reviewService.GetReviews(id);
            return View(book);
        }
        #endregion

        #region ManageBook
        [HttpPost]
        public async Task<IActionResult> ManageBook(BookDTO book)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = book.Id > 0 ? await _bookService.UpdateBook(book) : await _bookService.AddBook(book);
            return Json(result);
        }
        #endregion

        #region GetBook
        [HttpGet]
        public async Task<IActionResult> GetBook(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _bookService.GetBook(id);
            return Json(result);
        }
        #endregion

        #region DeleteBook
        public async Task<IActionResult> DeleteBook(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _bookService.DeleteBook(id);
            return Json(result);
        }
        #endregion
    }
}
