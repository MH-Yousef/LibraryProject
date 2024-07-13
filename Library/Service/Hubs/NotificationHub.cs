using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendFriendRequest(int UserId)
        {
            await Clients.User(UserId.ToString()).SendAsync("ReceiveFriendRequest");
        }

        public async Task AcceptFriendRequest(int userId)
        {
            await Clients.User(userId.ToString()).SendAsync("ReceiveFriendRequestAccepted");
        }
        public async Task RejectFriendRequest(int userId)
        {
            await Clients.User(userId.ToString()).SendAsync("ReceiveFriendRequestRejected");
        }
        public async Task CancelFriendRequest(int userId)
        {
            await Clients.User(userId.ToString()).SendAsync("ReceiveFriendRequestCanceled");
        }

        
    }
}
