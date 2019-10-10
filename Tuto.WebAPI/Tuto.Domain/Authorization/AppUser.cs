using System;
using System.Collections.Generic;
using System.Security.Claims;
using Tuto.Domain.Models;

namespace Tuto.Domain.Authorization
{
    public class AppUser : ClaimsPrincipal
    {
        public User User { get; private set; }

        public string IP { get; private set; }

        public HashSet<string> Roles { get; private set; }

        public DateTime CreatedTime { get; set; }

        public override bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        public AppUser(HashSet<string> roles, string ip, User user, DateTime creatingTime)
        {
            User = user;
            IP = ip;
            Roles = roles;
            CreatedTime = creatingTime;
        }
    }
}
