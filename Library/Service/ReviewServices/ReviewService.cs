using Core.Domains;
using Core.Enums;
using Data.Context;
using Data.DTOs.Review;
using Microsoft.EntityFrameworkCore;
using Service.BaseResponses;
using Service.FrenidShipServices;
using Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ReviewServices
{
    public class ReviewService : BaseServices<ReviewService>, IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IAppUserService _appUserService;
        private readonly IFreindShipService _freindShipService;
        public ReviewService(AppDbContext context, IAppUserService appUserService, IFreindShipService freindShipService)
        {
            _context = context;
            _appUserService = appUserService;
            _freindShipService = freindShipService;
        }
        #region Add Review
        public async Task<ResponseResult> AddReview(ReviewDTO review)
        {
            try
            {
                var model = new Review
                {
                    Content = review.Content,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ShareType = (ReviewShareType)review.type,
                    UserId = review.UserId,
                    BookId = review.BookId
                };
                _context.Reviews.Add(model);
                await _context.SaveChangesAsync();
                return Success(Message: "The review has been added successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion

        #region Delete Review
        public async Task<ResponseResult> DeleteReview(int id)
        {
            try
            {
                var result = await GetReview(id);
                if (result == null)
                {
                    return Error();
                }
                var model = new Review
                {
                    Id = result.Id,
                    Content = result.Content,
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt,
                    ShareType = (ReviewShareType)result.type,
                    UserId = result.UserId
                };
                _context.Reviews.Remove(model);
                await _context.SaveChangesAsync();
                return Success(Message: "The review has been deleted successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion

        #region Get Review
        public async Task<ReviewDTO> GetReview(int id)
        {
            try
            {
                var model = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
                if (model == null)
                {
                    return null;
                }
                return new ReviewDTO
                {
                    Id = model.Id,
                    Content = model.Content,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                    type = (int)model.ShareType,
                    UserId = model.UserId
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get Reviews
        public async Task<List<ReviewDTO>> GetReviews(int id)
        {
            var user = await _appUserService.GetUser();
            var userFreinds = await _freindShipService.GetFreinds(user.Id);

            var userReviews = await _context.Reviews.Where(x => x.UserId == user.Id && x.IsDeleted == false).Select(x => new ReviewDTO
            {
                Id = x.Id,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                type = (int)x.ShareType,
                UserId = x.UserId
            }).ToListAsync();
            var type = ReviewShareType.Public;
            var publicReviews = await _context.Reviews.Where(x => x.IsDeleted == false && x.BookId == id && x.ShareType == ReviewShareType.Public).Select(x => new ReviewDTO
            {
                Id = x.Id,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                type = (int)x.ShareType,
                UserId = x.UserId
            }).ToListAsync();

            var FriendsReviews = await _context.Reviews.Where(x => userFreinds.Select(x => x.Id).Contains(x.UserId) && x.IsDeleted == false && x.ShareType == ReviewShareType.Friends).Select(x => new ReviewDTO
            {
                Id = x.Id,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                type = (int)x.ShareType,
                UserId = x.UserId
            }).ToListAsync();

            var result = userReviews
    .Concat(publicReviews)
    .Concat(FriendsReviews)
    .ToLookup(x => x.Id)
    .Select(g => g.First())
    .ToList();

            return result;

        }
        #endregion

        #region Update Review
        public async Task<ResponseResult> UpdateReview(ReviewDTO review)
        {
            try
            {
                if (review == null)
                {
                    return Error();
                }
                var model = new Review
                {
                    Id = review.Id,
                    Content = review.Content,
                    UpdatedAt = DateTime.Now,
                    ShareType = (ReviewShareType)review.type,
                    UserId = review.UserId
                };
                _context.Update(model);
                await _context.SaveChangesAsync();
                return Success(Message: "The review has been updated successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion
    }
}
