using Data.DTOs.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.BaseResponses;
using Service.CategoryServices;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<CategoryDTO>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return categories;
        }
        [HttpGet("{id}")]
        public async Task<CategoryDTO> GetCategory(int id)
        {
            var category = await _categoryService.GetCategory(id);
            return category;
        }
        [HttpPost]
        public async Task<ResponseResult> AddCategory([FromBody] CategoryDTO category)
        {
            var result = await _categoryService.AddCategory(category);
            return result;
        }

        [HttpPut]
        public async Task<ResponseResult> UpdateCategory([FromBody] CategoryDTO category)
        {
            var result = await _categoryService.UpdateCategory(category);
            return result;
        }
        [HttpDelete]
        public async Task<ResponseResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            return result;
        }
    }
}
