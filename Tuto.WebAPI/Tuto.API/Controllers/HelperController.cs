using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public struct MessageCommunication
    {
        public Guid UserId { get; set; }
        public Guid TeacherId { get; set; }
    }

    public class HelperController : ControllerBase
    {
        private readonly IRepository<ChatMessage> _chatMessages;

        public HelperController(IRepository<ChatMessage> chatMessages)
        {
            _chatMessages = chatMessages;
        }

        [HttpPost]
        public async Task<bool> HaveAnyCommonMessages([FromBody] MessageCommunication guids)
        {
            var userId = guids.UserId;
            var teacherId = guids.TeacherId;
            var haveAnyCommonMessages = await _chatMessages.Read().AnyAsync(cm => (cm.SenderId == userId && cm.RecipientId == teacherId) ||
            (cm.SenderId == teacherId && cm.RecipientId == userId));
            return haveAnyCommonMessages;
        }
    }
}
