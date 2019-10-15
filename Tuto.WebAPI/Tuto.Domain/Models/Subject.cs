using System;
using System.Collections.Generic;
using System.Text;
using Tuto.Domain.Models.Interfaces;

namespace Tuto.Domain.Models
{
    public class Subject : IODataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
