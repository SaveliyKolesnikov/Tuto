using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;
using Tuto.Services.Interfaces;

namespace Tuto.Services
{
    public class ChatManager : IChatManager
    {
        private readonly IRepository<ChatMessage> _chatMessages;

        public ChatManager(IRepository<ChatMessage> chatMessages)
        {
            _chatMessages = chatMessages;
        }

        public Task<bool> HaveAnyCommonMessages(Guid user1, Guid user2) =>
            _chatMessages.Read().AnyAsync(cm => cm.SenderId == user1 && cm.RecipientId == user2 ||
                                                         cm.SenderId == user2 && cm.RecipientId == user1);
    }
}