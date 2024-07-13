using Data.DTOs.Shlef;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ShelfServices;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        private readonly IShelfService _shelfService;

        public ShelfController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        public async Task<IActionResult> AddShelf(ShelfDTO shelf)
        {
            var result = await _shelfService.AddShelf(shelf);
            return Ok(result);
        }

        public async Task<IActionResult> UpdateShelf(ShelfDTO shelf)
        {
            var result = await _shelfService.UpdateShelf(shelf);
            return Ok(result);
        }

        public async Task<IActionResult> DeleteShelf(int id)
        {
            var result = await _shelfService.DeleteShelf(id);
            return Ok(result);
        }

        public async Task<IActionResult> GetShelf(int id)
        {
            var result = await _shelfService.GetShelf(id);
            return Ok(result);
        }

        public async Task<IActionResult> GetShelves()
        {
            var result = await _shelfService.GetShelves();
            return Ok(result);
        }
    }
}
