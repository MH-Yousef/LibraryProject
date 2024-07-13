using Data.DTOs.FrenidShip;
using Data.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.FrenidShipServices;
using Service.Hubs;
using Service.User;

namespace Library.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IFreindShipService _freindShipService;
        private readonly IAppUserService _userService;
        public NotificationController(IHubContext<NotificationHub> hubContext, IFreindShipService freindShipService, IAppUserService userService)
        {
            this._hubContext = hubContext;
            _freindShipService = freindShipService;
            _userService = userService;
        }

        #region SendFriendRequest
        [HttpPost]
        public async Task<IActionResult> SendRequest(RequestDTO request)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            await _hubContext.Clients.User(request.TargetUserId.ToString()).SendAsync("ReceiveFriendRequest", request.SenderId, request.fullName,request.Image);
            var toastData = new
            {
                Title = "Friend Request",
                Message = $"You have Send friend request to {request.fullName}",
                Date = DateTime.Now.ToString("g") 
            };

            var model = new FreindShipDTO
            {
                ReceiverId = request.TargetUserId,
                SenderId = request.SenderId
            };
            try
            {
                var result = await _freindShipService.AddFreind(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Json(toastData);
        
        }
        #endregion

        #region AcceptFriendRequest

        [HttpPost]
        public async Task<IActionResult> AcceptRequest(RequestDTO Accept)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            // Friend request'i kabul eden kullanıcıya bildirim gönder
            await _hubContext.Clients.User(Accept.TargetUserId.ToString()).SendAsync("ReceiveFriendRequestAccepted", Accept.fullName);
            var toast = new
            {
                Title = "Friend Request",
                Message = $"You have accepted friend request from {Accept.fullName}",
                Date = DateTime.Now.ToString("g")
            };
            try
            {
                var result = await _freindShipService.AcceptFreindRequest(Accept.TargetUserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Json(toast);
        }
        #endregion

        #region RejectFriendRequest
        [HttpPost]
        public async Task<IActionResult> RejectRequest(RequestDTO Reject)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            // Friend request'i reddeden kullanıcıya bildirim gönder
            await _hubContext.Clients.User(Reject.TargetUserId.ToString()).SendAsync("ReceiveFriendRequestRejected", Reject.fullName);
            var toast = new
            {
                Title = "Friend Request",
                Message = $"You have rejected friend request from {Reject.fullName}",
                Date = DateTime.Now.ToString("g")
            };
            try
            {
                var result = await _freindShipService.RejectFreindRequest(Reject.TargetUserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Json(toast);
        }
        #endregion

        #region CancelFriendRequest
        [HttpPost]
        public async Task<IActionResult> CancelRequest(RequestDTO Cancel)
        {
            var IsSgined = _userService.IsSignIn();
            if (!IsSgined) return RedirectToAction("Login", "Account");

            // Friend request'i iptal eden kullanıcıya bildirim gönder
            await _hubContext.Clients.User(Cancel.TargetUserId.ToString()).SendAsync("ReceiveFriendRequestCanceled", Cancel.fullName);
            var toast = new
            {
                Title = "Friend Request",
                Message = $"You have canceled friend request to {Cancel.fullName}",
                Date = DateTime.Now.ToString("g")
            };
            try
            {
                var result = await _freindShipService.CancelFreindRequest(Cancel.TargetUserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Json(toast);
        }
        #endregion
    }
}
