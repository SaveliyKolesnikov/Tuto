using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Tuto.Domain;
using Tuto.Services.Interfaces;

namespace Tuto.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IAppUserManager _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        public static readonly ConcurrentDictionary<string, Guid> Connections = new ConcurrentDictionary<string, Guid>();

        public ChatHub(IAppUserManager userManager,
            ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        public override Task OnConnectedAsync()
        {
            if (_userManager.TryGetUserId(Context.User, out var appUserId))
            {
                Connections.TryAdd(Context.ConnectionId, appUserId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Connections.TryRemove(Context.ConnectionId, out _);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
