using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tuto.API.Authorization;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class ChatMessagesController : ODataController
    {
        private readonly IRepository<ChatMessage> _chatMessagesRepository;
        public ChatMessagesController(IRepository<ChatMessage> chatMessagesRepository)
        {
            _chatMessagesRepository = chatMessagesRepository;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_chatMessagesRepository.Read());
        }

        [EnableQuery]
        public async Task<IActionResult> Get(Guid key)
        {
            return Ok(await _chatMessagesRepository.Read().FirstOrDefaultAsync(c => c.ChatMessageId == key));
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody]ChatMessage chatMessage)
        {
            _chatMessagesRepository.Create(chatMessage);
            await _chatMessagesRepository.Commit();
            return Created(chatMessage);
        }

        public async Task<IActionResult> Put([FromODataUri]Guid key, [FromBody]ChatMessage chatMessage)
        {
            var messageExists = await _chatMessagesRepository.Read().AnyAsync(l => l.ChatMessageId == key);
            if (!messageExists)
            {
                return NotFound();
            }
            chatMessage.ChatMessageId = key;
            _chatMessagesRepository.Update(chatMessage);
            await _chatMessagesRepository.Commit();
            return Updated(chatMessage);
        }

        public async Task<IActionResult> Delete([FromODataUri]Guid key)
        {
            var messageFromDb = await _chatMessagesRepository.Read().FirstOrDefaultAsync(l => l.ChatMessageId == key);
            if (messageFromDb is null)
            {
                return NotFound();
            }
            _chatMessagesRepository.Delete(messageFromDb);
            await _chatMessagesRepository.Commit();
            return NoContent();
        }
    }
}
