using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        private static readonly ICollection<ConsultantViewModel> Consultants = new List<ConsultantViewModel>();

        public ChatHub(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
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
            await Clients.Group("consultants").SendAsync("SendToConsultants", text, username);
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
                    await Clients.All.SendAsync("SwichConsultant", true);
                }
                Consultants.Add(consultant);
                await Groups.AddToGroupAsync(Context.ConnectionId, "consultants");
            }
            else
            {
                if (Consultants.Any())
                {
                    await Clients.Client(Context.ConnectionId).SendAsync("SwichConsultant", true);
                }
                else
                {
                    await Clients.Client(Context.ConnectionId).SendAsync("SwichConsultant", false);
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
                    await Clients.All.SendAsync("SwichConsultant", false);
                } 
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "clients");
            await base.OnDisconnectedAsync(exception);
        }
    }
}