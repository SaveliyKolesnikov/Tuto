using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuto.Domain.Models
{
    public class Lesson
    {
        public Guid LessonId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime LessontTime { get; set; }
        public string Text { get; set; }
        [ForeignKey("TeacherId")]
        public User Teacher { get; set; }
        [ForeignKey("StudentId")]
        public User Student { get; set; }
    }
}
