using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tuto.API.Authorization;
using Tuto.API.Hubs;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;
using Tuto.Services.Interfaces;

namespace Tuto.API.Controllers
{
    [AuthFilter]
    public class ChatMessagesController : ODataControllerBase<ChatMessage>
    {
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly IRepository<User> _userRepository;
        private readonly IAppUserManager _userManager;

        public ChatMessagesController(IRepository<ChatMessage> entityRepository,  
            IHubContext<ChatHub> chatHub, IRepository<User> userRepository, IAppUserManager userManager) : base(entityRepository)
        {
            _chatHub = chatHub;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public override async Task<IActionResult> Post(ChatMessage entity)
        {
            var (isValid, errorMessage) = await IsMessageValid(entity);
            if (!isValid) return BadRequest(errorMessage);

            var actionResult = await base.Post(entity);
            try
            {
                await SendMessageToHubAsync(entity);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine(e);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
            return actionResult;
        }

        private async Task SendMessageToHubAsync(ChatMessage message)
        {
            var recipientConnection = ChatHub.Connections.FirstOrDefault(c => c.Value == message.RecipientId);
            if (!recipientConnection.Equals(default(KeyValuePair<string, Guid>)))
            {
                await _chatHub.Clients.Client(recipientConnection.Key).SendAsync("receiveMessage", message);
            }
            var senderConnection = ChatHub.Connections.FirstOrDefault(c => c.Value == message.SenderId);
            if (!senderConnection.Equals(default(KeyValuePair<string, Guid>)))
            {
                await _chatHub.Clients.Client(senderConnection.Key).SendAsync("receiveMessage", message);
            }

        }

        private async Task<(bool isValid, string errorMessage)> IsMessageValid(ChatMessage message)
        {
            _userManager.TryGetUserId(User, out var appUserId);
            var sender = await _userRepository.Read().Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == appUserId);
            if (sender.Roles.Any(r => r.Name == AuthRoles.Teacher) && !await _entityRepository.Read()
                    .AnyAsync(m => m.SenderId == message.SenderId && m.RecipientId == message.RecipientId))
            {
                return (false, "Teacher cannot send first message.");
            } 

            if (message.SenderId != sender.Id)
                return (false, "Fake sender id.");

            if (!await _userRepository.Read().AnyAsync(u => u.Id == message.RecipientId))
                return (false, $"User with id {message.RecipientId} doesn't exist.");

            message.Text = new Regex(@"&nbsp;?").Replace(message.Text, " ");
            message.Text = HttpUtility.HtmlDecode(message.Text);
            if (string.IsNullOrWhiteSpace(message.Text))
                return (false, "Message cannot be empty.");

            if (message.Text.Length > 4096)
                return (false, "Message text length must be less then 4096.");

            return (true, string.Empty);
        }
    }
}