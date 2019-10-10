using System;
using System.Collections.Generic;

namespace Tuto.Domain.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
