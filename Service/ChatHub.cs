using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Service
{
    public class ChatHub : Hub
    {
        public async Task Send(string text, string username)
        {
            await Clients.All.SendAsync("Send", text, username);
        }
    }
}