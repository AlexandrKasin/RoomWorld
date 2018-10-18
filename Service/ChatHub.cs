﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Service
{
    public class ChatHub : Hub
    {
        public async Task Send(string message, string userName)
        {
            await Clients.All.SendAsync("Send", message, userName);
        }
    }
}