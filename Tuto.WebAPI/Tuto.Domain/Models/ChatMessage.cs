using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    public class ChatMessage : IODataEntity
    {
        public Guid Id { get; set; }
        public DateTime SendTime { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? RecipientId { get; set; }
        public string Text { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }
    }
}
