using Data.DTOs.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ReviewServices;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTO review)
        {
            var result = await _reviewService.AddReview(review);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewDTO review)
        {
            var result = await _reviewService.UpdateReview(review);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReview(id);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var result = await _reviewService.GetReview(id);
            return Ok(result);
        }
        [HttpGet("GetReviews/{id}")]
        public async Task<IActionResult> GetReviews(int id)
        {
            var result = await _reviewService.GetReviews(id);
            return Ok(result);
        }
    }
}
