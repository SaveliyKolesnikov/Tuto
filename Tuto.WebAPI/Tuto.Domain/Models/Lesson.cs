using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    public class Lesson : IODataEntity
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime LessonTime { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid SubjectId { get; set; }
        public string Text { get; set; }
        [ForeignKey("TeacherId")]
        public User Teacher { get; set; }
        [ForeignKey("StudentId")]
        public User Student { get; set; }
        public Subject Subject { get; set; }
    }
}
