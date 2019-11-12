using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tuto.Domain.Authorization;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;
using Tuto.Services.Interfaces;

namespace Tuto.API.Authorization
{
    public class AuthMiddleware
    {
        private static TimeSpan UpdateRoleCycleDuration { get; } = TimeSpan.FromMinutes(15);
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ISessionStorage<AppUser> storage, IRepository<User> repository)
        {
            var cookie = context.Request.Cookies["sessionId"];

            if (!string.IsNullOrEmpty(cookie))
            {
                var ip = context.Connection.RemoteIpAddress.ToString();
                var sessionId = Guid.Parse(cookie);
                var user = storage.Get(sessionId);

                if (user?.IP?.Equals(ip, StringComparison.InvariantCulture) ?? false)
                {
                    if (IsUpdatedRole(user, DateTime.UtcNow))
                    {
                        var newAppUser = await UpdateRoleAsync(user, repository);
                        storage.Set(sessionId, newAppUser);
                        user = newAppUser;
                    }

                    context.User = user;
                }
            }

            await _next.Invoke(context);
        }

        private static async Task<AppUser> UpdateRoleAsync(AppUser user, IRepository<User> repository)
        {
            var newUser = await repository.Read().Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == user.User.Id);
            var newRoles = new HashSet<string>(newUser.Roles.Select(x => x.Name));
            var newAppUser = new AppUser(newRoles, user.IP, newUser, DateTime.UtcNow);

            return newAppUser;
        }

        private static bool IsUpdatedRole(AppUser user, DateTime currentTime) => (currentTime - user.CreatedTime) > UpdateRoleCycleDuration;
    }
}
