using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tuto.Services.Interfaces;

namespace Tuto.API.Controllers
{
    public struct MessageCommunication
    {
        public Guid UserId { get; set; }
        public Guid TeacherId { get; set; }
    }

    public class HelperController : ControllerBase
    {
        private readonly IChatManager _chatManager;

        public HelperController(IChatManager chatManager)
        {
            _chatManager = chatManager;
        }

        [HttpPost]
        public async Task<bool> HaveAnyCommonMessages([FromBody] MessageCommunication guids)
        {
            var userId = guids.UserId;
            var teacherId = guids.TeacherId;
            var haveAnyCommonMessages = await _chatManager.HaveAnyCommonMessages(userId, teacherId);
            return haveAnyCommonMessages;
        }
    }
}
