using System;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    public class City : IODataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
