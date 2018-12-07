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
        private static readonly ICollection<string> FreeClients = new List<string>();

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
            await Clients.Client(id).SendAsync("Send", text, username);
        }

        public override async Task OnConnectedAsync()
        {
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;

            if (role == "Consultant")
            {
                var user = await _userService.GetUserByEmailAsync(email);
                var consultant = new ConsultantViewModel
                {
                    IdSignalR = Context.ConnectionId,
                    Email = email,
                    Name = user.Name,
                    UsersInChat = new List<string>(FreeClients)
                };
                FreeClients.Clear();
                if (!Consultants.Any())
                {
                    await Clients.All.SendAsync("SwichConsultant", Context.ConnectionId, user.Name, email);
                }

                Consultants.Add(consultant);
            }
            else
            {
                if (Consultants.Any())
                {
                    var freeConsultant = Consultants.FirstOrDefault();
                    foreach (var conultant in Consultants)
                    {
                        if (freeConsultant != null && freeConsultant.UsersInChat?.Count > conultant.UsersInChat?.Count)
                        {
                            freeConsultant = conultant;
                        }
                    }

                    freeConsultant?.UsersInChat?.Add(Context.ConnectionId);
                    await Clients.Client(Context.ConnectionId).SendAsync("SwichConsultant", freeConsultant?.IdSignalR,
                        freeConsultant?.Name, freeConsultant?.Email);
                }
                else
                {
                    FreeClients.Add(Context.ConnectionId);
                    await Clients.Client(Context.ConnectionId).SendAsync("SwichConsultant", null, null, null);
                }
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;
            if (role == "Consultant")
            {
                var user = await _userService.GetUserByEmailAsync(email);
                var oflineConsultant = Consultants.SingleOrDefault(x => x.Email == user.Email);
                Consultants.Remove(oflineConsultant);
                if (Consultants.Any())
                {
                    var freeConsultant = Consultants.FirstOrDefault();
                    foreach (var conultant in Consultants)
                    {
                        if (freeConsultant != null && freeConsultant.UsersInChat?.Count > conultant.UsersInChat?.Count)
                        {
                            freeConsultant = conultant;
                        }
                    }


                    if (oflineConsultant != null && oflineConsultant.UsersInChat.Any())
                    {
                        foreach (var userInChat in oflineConsultant.UsersInChat)
                        {
                            await Clients.Client(userInChat).SendAsync("SwichConsultant", freeConsultant?.IdSignalR,
                                freeConsultant?.Name, freeConsultant?.Email);
                        }
                    }
                }
                else
                {
                    if (oflineConsultant != null && oflineConsultant.UsersInChat.Any())
                    {
                        foreach (var userInChat in oflineConsultant.UsersInChat)
                        {
                            await Clients.Client(userInChat).SendAsync("SwichConsultant", null, null, null);
                        }
                    }
                }
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}