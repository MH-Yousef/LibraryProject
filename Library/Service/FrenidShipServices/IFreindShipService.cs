using Core.Domains;
using Data.DTOs.FrenidShip;
using Data.DTOs.UserDTO;
using Service.BaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FrenidShipServices
{
    public interface IFreindShipService
    {
        public Task<ResponseResult> AddFreind(FreindShipDTO freindShip);
        public Task<ResponseResult> RemoveFreind(int FriendShip);
        public Task<List<UserDTO>> GetFreinds(int id);
        public Task<List<ApplicationUser>> GetFreindRequestsReceive(int userId);
        public Task<List<ApplicationUser>> GetFreindRequestsSent(int userId);
        public Task<ResponseResult> AcceptFreindRequest(int userId);
        public Task<ResponseResult> CancelFreindRequest(int userId);
        public Task<ResponseResult> RejectFreindRequest(int userId);
    }
}
