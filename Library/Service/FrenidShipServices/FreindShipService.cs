using Core.Domains;
using Data.Context;
using Data.DTOs.FrenidShip;
using Data.DTOs.UserDTO;
using Microsoft.EntityFrameworkCore;
using Service.BaseResponses;
using Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FrenidShipServices
{
    public class FreindShipService : BaseServices<FreindShipService>, IFreindShipService
    {
        private readonly AppDbContext _context;
        private readonly IAppUserService _appUserService;

        public FreindShipService(AppDbContext context, IAppUserService appUserService)
        {
            _context = context;
            _appUserService = appUserService;
        }

        public async Task<ResponseResult> AcceptFreindRequest(int userId)
        {
            var user = await _appUserService.GetUser();
            var result = await _context.Friendships.Where(x => x.ReceiverId == user.Id && x.SenderId == userId).FirstOrDefaultAsync();
            result.IsAccepted = true;
            result.AcceptanceDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return Success();
        }

        #region add freind
        public async Task<ResponseResult> AddFreind(FreindShipDTO freindShip)
        {
            var model = new Friendship
            {
                Id = freindShip.Id,
                SenderId = freindShip.SenderId,
                ReceiverId = freindShip.ReceiverId,
                RequestDate = DateTime.Now
            };
            _context.Friendships.Add(model);
            await _context.SaveChangesAsync();
            return Success();
        }
        #endregion
        #region cancel freind request
        public async Task<ResponseResult> CancelFreindRequest(int userId)
        {
            var user = await _appUserService.GetUser();
            var result = _context.Friendships.FirstOrDefault(x => x.ReceiverId == userId && x.SenderId == user.Id || x.ReceiverId == user.Id && x.SenderId == userId);
            _context.Friendships.Remove(result);
            await _context.SaveChangesAsync();
            return Success();
        }
        #endregion

        #region Reject Freind Request
        public async Task<ResponseResult> RejectFreindRequest(int userId)
        {
            var user = await _appUserService.GetUser();
            var result = _context.Friendships.FirstOrDefault(x => x.ReceiverId == user.Id && x.SenderId == userId);
            _context.Friendships.Remove(result);
            await _context.SaveChangesAsync();
            return Success();
        }
        #endregion

        // Get all friend requests that are not accepted
        public async Task<List<ApplicationUser>> GetFreindRequestsReceive(int userId)
        {
            var result = await _context.Friendships.Where(x => x.ReceiverId == userId && x.IsAccepted == false).Select(x => x.Sender).ToListAsync();
            return result;
        }
        // Get all friend requests that are sent
        public async Task<List<ApplicationUser>> GetFreindRequestsSent(int userId)
        {
            var result = await _context.Friendships.Where(x => x.SenderId == userId && x.IsAccepted == false).Select(x => x.Receiver).ToListAsync();
            return result;
        }
        public async Task<List<UserDTO>> GetFreinds(int id)
        {
            var friendsAsSender = await _context.Friendships
        .Where(x => x.SenderId == id && x.IsAccepted)
        .Select(x => x.Receiver)
        .Select(x => new UserDTO
        {
            Id = x.Id,
            FullName = x.FullName,
            Image = x.Image
        })
        .ToListAsync();

            var friendsAsReceiver = await _context.Friendships
                .Where(x => x.ReceiverId == id && x.IsAccepted)
                .Select(x => x.Sender)
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Image = x.Image
                })
                .ToListAsync();

            var allFriends = friendsAsSender.Concat(friendsAsReceiver).ToList();
            return allFriends;
        }

        public async Task<ResponseResult> RemoveFreind(int FriendShip)
        {
            var FreindShip = _context.Friendships.Find(FriendShip);
            _context.Friendships.Remove(FreindShip);
            await _context.SaveChangesAsync();
            return Success();
        }
    }
}
