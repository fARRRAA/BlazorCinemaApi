using CinemaDigestApi.Model;
using Microsoft.AspNetCore.SignalR;

namespace CinemaDigestApi.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(User user,MovieChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage",user,message); 
        }
    }
}
