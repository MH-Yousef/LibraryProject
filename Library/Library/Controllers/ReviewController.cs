using Data.DTOs.Review;
using Microsoft.AspNetCore.Mvc;
using Service.ReviewServices;
using Service.User;

namespace Library.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IAppUserService _userService;

        public ReviewController(IReviewService reviewService, IAppUserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        #region Add Review
        public async Task<IActionResult> AddReview(ReviewDTO review)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _reviewService.AddReview(review);
            return Json(result);
        }
        #endregion

        #region Update Review
        public async Task<IActionResult> UpdateReview(ReviewDTO review)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _reviewService.UpdateReview(review);
            return Json(result);
        }
        #endregion

        #region Delete Review
        public async Task<IActionResult> DeleteReview(int id)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            var result = await _reviewService.DeleteReview(id);
            return Json(result);
        }
        #endregion

        #region Get Review
        public async Task<IActionResult> GetReview(int id)
        {
            var result = await _reviewService.GetReview(id);
            return Json(result);
        }
        #endregion

        #region Get Reviews
        public async Task<IActionResult> GetReviews(int bookId)
        {
            var result = await _reviewService.GetReviews(bookId);
            return Json(result);
        }
        #endregion
    }
}
