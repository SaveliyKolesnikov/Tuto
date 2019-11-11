using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Tuto.Domain.Authorization;
using Tuto.Services.Interfaces;

namespace Tuto.Services
{
    public class AppUserManager : IAppUserManager
    {
        public bool TryGetUserId(ClaimsPrincipal user, out Guid appUserId)
        {
            appUserId = Guid.Empty;
            if (user is AppUser appUser)
            {
                appUserId = appUser?.User?.Id ?? Guid.Empty;
            }
            return appUserId != Guid.Empty;
        }
    }
}
