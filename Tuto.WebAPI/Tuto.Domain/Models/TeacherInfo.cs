using System;

namespace Tuto.Domain.Models
{
    public class TeacherInfo
    {
        public Guid TeacherInfoId { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
