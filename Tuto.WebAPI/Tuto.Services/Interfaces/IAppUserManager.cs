using System;
using System.Security.Claims;

namespace Tuto.Services.Interfaces
{
    public interface IAppUserManager
    {
        bool TryGetUserId(ClaimsPrincipal user, out Guid appUserId);
    }
}