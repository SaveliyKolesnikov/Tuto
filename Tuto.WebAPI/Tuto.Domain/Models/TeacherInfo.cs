using System;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    public class TeacherInfo : IODataEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
