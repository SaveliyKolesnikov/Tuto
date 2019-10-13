using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class ChatMessagesController : ODataControllerBase<ChatMessage>
    {
        public ChatMessagesController(IRepository<ChatMessage> entityRepository) : base(entityRepository)
        {
        }
    }
}