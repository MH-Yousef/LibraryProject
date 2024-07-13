using Data.DTOs.Category;
using Microsoft.AspNetCore.Mvc;
using Service.CategoryServices;
using Service.SectionServices;
using Service.User;

namespace Library.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISectionService _sectionService;
        private readonly IAppUserService _userService;

        public CategoryController(ICategoryService categoryService, ISectionService sectionService, IAppUserService appUserService)
        {
            _categoryService = categoryService;
            _sectionService = sectionService;
            _userService = appUserService;
        }

        public async Task<IActionResult> Index()
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var categories = await _categoryService.GetCategories();
            ViewBag.dataSource = categories;
            ViewBag.Sections = await _sectionService.GetSections();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>  GetCategory(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _categoryService.GetCategory(id);
            return Json(result);
        }

        public async Task<IActionResult> ManageCategory(CategoryDTO categoryDTO)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            if (categoryDTO.Id > 0)
            {
                var result = await _categoryService.UpdateCategory(categoryDTO);
                return Json(result);
            }
            if (categoryDTO != null)
            {
                var result = await _categoryService.AddCategory(categoryDTO);
                return Json(result);

            }
            return View();
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _categoryService.DeleteCategory(id);
            return Json(result);
        }
    }
}
