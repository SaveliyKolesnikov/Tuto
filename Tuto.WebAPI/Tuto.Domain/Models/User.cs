using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    public class User : IODataEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Picture { get; set; }

        public TeacherInfo TeacherInfo { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Guid? RegionId { get; set; }
        public Region Region { get; set; }
        public Guid? CityId { get; set; }
        public City City { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
