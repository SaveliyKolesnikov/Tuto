using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    public class TeacherInfo : IODataEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SubjectId { get; set; }
        [Range(0, Int32.MaxValue)]
        public int MinimumWage { get; set; }
        public User User { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}
