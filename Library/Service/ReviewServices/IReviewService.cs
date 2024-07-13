using Data.DTOs.Review;
using Service.BaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ReviewServices
{
    public interface IReviewService 
    {
        public Task<ReviewDTO> GetReview(int id);
        public Task<List<ReviewDTO>> GetReviews(int id);
        public Task<ResponseResult> AddReview(ReviewDTO review);
        public Task<ResponseResult> UpdateReview(ReviewDTO review);
        public Task<ResponseResult> DeleteReview(int id);
    }
}
