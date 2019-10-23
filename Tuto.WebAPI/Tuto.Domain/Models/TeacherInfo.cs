using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    [Flags]
    public enum WeekDays
    {
        Monday = 0b_0000_0001,
        Tuesday = 0b_0000_0010,
        Wednesday = 0b_0000_0100,
        Thursday = 0b_0000_1000,
        Friday = 0b_0001_0000,
        Sunday = 0b_0010_0000,
        Saturday = 0b_0100_0000,
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
