using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tuto.Domain.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Uri Picture { get; set; }

        public TeacherInfo TeacherInfo { get; set; }
        public Point Location { get; set; }
        public string Description { get; set; }
        public Guid? RegionId { get; set; }
        public Region Region { get; set; }
        public Guid? CityId { get; set; }
        public City City { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
