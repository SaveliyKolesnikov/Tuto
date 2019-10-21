using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    [Flags]
    public enum WeekDays
    {
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Sunday = 64,
        Saturday = 128,
    }
    public class TeacherInfo : IODataEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Range(0, double.MaxValue)]
        public double MinimumWage { get; set; }
        public WeekDays? PreferredDaysOfWeek { get;set; }
        public User User { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}
