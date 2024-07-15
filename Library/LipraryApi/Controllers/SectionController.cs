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
        [HttpPost]
        public async Task<IActionResult> AddSection([FromBody] SectionDTO section)
        {
            var result = await _sectionService.AddSection(section);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSection([FromBody] SectionDTO section)
        {
            var result = await _sectionService.UpdateSection(section);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var result = await _sectionService.DeleteSection(id);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSection(int id)
        {
            var result = await _sectionService.GetSection(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetSections()
        {
            var result = await _sectionService.GetSections();
            return Ok(result);
        }
    }
}
