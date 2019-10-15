using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    public class TeacherSubject: IODataEntity
    {
        public Guid Id { get; set; }
        public Guid TeacherInfoId { get; set; }
        [ForeignKey("TeacherInfoId")]
        public TeacherInfo TeacherInfo { get; set; }
    }
}
