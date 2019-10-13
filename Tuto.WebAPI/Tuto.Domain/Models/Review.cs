using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuto.Domain.Models
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        [Range(0, 5)]
        public int Mark { get; set; }
        public string Message { get; set; }
        public Guid CreatorId { get; set; }
        public Guid ForWhomId { get; set; }
        [ForeignKey("CreatorId")]
        public User Creator { get; set; }
        [ForeignKey("ForWhomId")]
        public User ForWhom { get; set; }
    }
}
