using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tuto.Domain;
using Tuto.Domain.Models;
using Tuto.Services.Interfaces;

namespace Tuto.API.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{
		private readonly IAppUserManager _userManager;
		private readonly ApplicationDbContext _applicationDbContext;
		private static readonly ConcurrentDictionary<string, Guid> Connections = new ConcurrentDictionary<string, Guid>();

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

		public async Task SendMessage(ChatMessage message)
		{
			_userManager.TryGetUserId(Context.User, out var appUserId);
			var sender = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == appUserId);

			if (message.SenderId != sender.Id)
				throw new AuthenticationException();

			if (!await _applicationDbContext.Users.AnyAsync(u => u.Id == message.RecipientId)) 
				throw new ArgumentException($"User with id {message.RecipientId} doesn't exist");

			message.Text = new Regex(@"&nbsp;?").Replace(message.Text, " ");
			message.Text = HttpUtility.HtmlDecode(message.Text);
			if (string.IsNullOrWhiteSpace(message.Text))
				throw new ArgumentNullException(nameof(message.Text));

			if (message.Text.Length > 4096)
				throw new ArgumentException("Message text length must be less then 4096");

			var recipientConnection = Connections.FirstOrDefault(c => c.Value == message.RecipientId);
			if (!recipientConnection.Equals(default(KeyValuePair<string, Guid>)))
			{
				await Clients.Client(recipientConnection.Key).SendAsync("receiveMessage", message);
			}
			await Clients.Caller.SendAsync("receiveMessage", message);
		}
	}
}
