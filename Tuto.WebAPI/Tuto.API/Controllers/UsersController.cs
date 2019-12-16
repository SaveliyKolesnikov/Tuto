using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class UsersController : ODataControllerBase<User>
    {
        private readonly IRepository<ChatMessage> _chatMessagesRepository;
        public UsersController(IRepository<User> entityRepository, IRepository<ChatMessage> chatMessagesrepository) : base(entityRepository)
        {
            _chatMessagesRepository = chatMessagesrepository;
        }

        public override async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var sentMessages = await _chatMessagesRepository.Read().Where(cm => cm.SenderId == key).ToListAsync();
            foreach(var message in sentMessages)
            {
                message.SenderId = null;
                _chatMessagesRepository.Update(message);
            }
            var recievedMessages = await _chatMessagesRepository.Read().Where(cm => cm.RecipientId == key).ToListAsync();
            foreach (var message in recievedMessages)
            {
                message.RecipientId = null;
                _chatMessagesRepository.Update(message);
            }
            return await base.Delete(key);
        }
    }
}
