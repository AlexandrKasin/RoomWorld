using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Service.DTO;
using Service.Services;

namespace Service.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        private static readonly IList<ConsultantViewModel> Consultants = new List<ConsultantViewModel>();

        public ChatHub(IHttpContextAccessor httpContextAccessor, IUserService userService, IMessageService messageService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _messageService = messageService;
        }

        public async Task Send(string text, string username)
        {
            await Clients.All.SendAsync("Send", text, username);
        }

        public async Task SendById(string id, string text, string username)
        {
            await Clients.Client(id).SendAsync("SendByID", text, username);
        }

        public async Task SendToConsultants(string text, string username)
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;
            await Clients.Group("consultants").SendAsync("SendToConsultants", text, username);
            var user = await _userService.GetUserByEmailAsync(email);
            await _messageService.AddMessageAsync(new Message {UserFrom = user, Text = text, UsernameTo = "consult"});   
        }

        public override async Task OnConnectedAsync()
        {
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;

            if (role != null && string.Equals(role, "consultant", StringComparison.CurrentCultureIgnoreCase))
            {
                var user = await _userService.GetUserByEmailAsync(email);
                var consultant = new ConsultantViewModel
                {
                    IdSignalR = Context.ConnectionId,
                    Email = email,
                    Name = user.Name,
                };
                if (!Consultants.Any())
                {
                    await Clients.All.SendAsync("stateConsultants", true);
                }
                Consultants.Add(consultant);
                await Groups.AddToGroupAsync(Context.ConnectionId, "consultants");
            }
            else
            {
                if (Consultants.Any())
                {
                    await Clients.Client(Context.ConnectionId).SendAsync("stateConsultants", true);
                }
                else
                {
                    await Clients.Client(Context.ConnectionId).SendAsync("stateConsultants", false);
                }
                await Groups.AddToGroupAsync(Context.ConnectionId, "clients");
            }
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;
            if (role != null && string.Equals(role, "consultant", StringComparison.CurrentCultureIgnoreCase))
            {
                Consultants.Remove(Consultants.SingleOrDefault(x => x.Email == email));
                if (!Consultants.Any())
                {
                    await Clients.All.SendAsync("stateConsultants", false);
                } 
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "clients");
            await base.OnDisconnectedAsync(exception);
        }
    }
}