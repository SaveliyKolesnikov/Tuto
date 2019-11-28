using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tuto.API.Hubs;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class ChatMessagesController : ODataControllerBase<ChatMessage>
    {
        private readonly ChatHub _chatHub;

        public ChatMessagesController(IRepository<ChatMessage> entityRepository, ChatHub chatHub) : base(entityRepository)
        {
            _chatHub = chatHub;
        }

        public override async Task<IActionResult> Post(ChatMessage entity)
        {
            var actionResult = await base.Post(entity);
            await _chatHub.SendMessage(entity);
            return actionResult;
        }
    }
}