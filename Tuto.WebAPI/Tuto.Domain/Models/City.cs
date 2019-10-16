using System;
using Tuto.Domain.Models.Interfaces;
using NetTopologySuite.Geometries;

namespace Tuto.Domain.Models
{
    public class City : IODataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
        public Guid RegionId { get; set; }
    }
}
