using Core.Domains;
using Data.DTOs.FrenidShip;
using Data.DTOs.UserDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.BaseResponses;
using Service.FrenidShipServices;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IFreindShipService _freindShipService;

        public NotificationController(IFreindShipService freindShipService)
        {
            _freindShipService = freindShipService;
        }
        [HttpPost]
        public async Task<ResponseResult> AcceptFreindRequest(int userId)
        {
            var result = await _freindShipService.AcceptFreindRequest(userId);
            return result;
        }
        [HttpPost]
        public async Task<ResponseResult> AddFreind([FromBody] FreindShipDTO freindShip)
        {
            var result = await _freindShipService.AddFreind(freindShip);
            return result;
        }
        [HttpPost]
        public async Task<ResponseResult> CancelFreindRequest(int userId)
        {
            var result = await _freindShipService.CancelFreindRequest(userId);
            return result;
        }
        [HttpPost]
        public async Task<ResponseResult> RejectFreindRequest(int userId)
        {
            var result = await _freindShipService.RejectFreindRequest(userId);
            return result;
        }
        [HttpGet("{userId}")]
        public async Task<List<ApplicationUser>> GetFreindRequestsReceive(int userId)
        {
            var result = await _freindShipService.GetFreindRequestsReceive(userId);
            return result;
        }
        [HttpGet("{userId}")]
        public async Task<List<ApplicationUser>> GetFreindRequestsSent(int userId)
        {
            var result = await _freindShipService.GetFreindRequestsSent(userId);
            return result;
        }
        [HttpGet("{id}")]
        public async Task<List<UserDTO>> GetFreinds(int id)
        {
            var result = await _freindShipService.GetFreinds(id);
            return result;
        }
        [HttpDelete("{FriendShip}")]
        public async Task<ResponseResult> RemoveFreind(int FriendShip)
        {
            var result = await _freindShipService.RemoveFreind(FriendShip);
            return result;
        }
    }
}
