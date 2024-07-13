using Data.DTOs.Section;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SectionServices;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        public async Task<IActionResult> AddSection(SectionDTO section)
        {
            var result = await _sectionService.AddSection(section);
            return Ok(result);
        }

        public async Task<IActionResult> UpdateSection(SectionDTO section)
        {
            var result = await _sectionService.UpdateSection(section);
            return Ok(result);
        }

        public async Task<IActionResult> DeleteSection(int id)
        {
            var result = await _sectionService.DeleteSection(id);
            return Ok(result);
        }

        public async Task<IActionResult> GetSection(int id)
        {
            var result = await _sectionService.GetSection(id);
            return Ok(result);
        }

        public async Task<IActionResult> GetSections()
        {
            var result = await _sectionService.GetSections();
            return Ok(result);
        }
    }
}
