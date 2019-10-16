using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;

namespace Tuto.Domain.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public TeacherInfo TeacherInfo { get; set; }
        public Point Location { get; set; }
        public string Description { get; set; }
        public Guid RegionId { get; set; }
        public Guid CityId { get; set; }
        public Region Region { get; set; }
        public City City { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
