using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Tuto.Services.Interfaces;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;
using Microsoft.Extensions.Options;
using Tuto.API.Configuration;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tuto.Domain.Authorization;

namespace Tuto.API.Controllers
{
    public class OAuthController : ControllerBase
    {
        private readonly ISessionStorage<AppUser> _storage;
        private readonly OAuthConfig _oAuthConfig;
        private readonly IRepository<User> _usersRepository;
        private readonly IGoogleOAuthService _googleOAuthService;

        public OAuthController(ISessionStorage<AppUser> storage, 
            IRepository<User> repository, 
            IGoogleOAuthService googleOAuthService,
            IOptions<OAuthConfig> appSettings)
        {
            _storage = storage;
            _usersRepository = repository;
            _googleOAuthService = googleOAuthService;
            _oAuthConfig = appSettings.Value;
        }

        [HttpGet]
        [Route("OAuth")]
        public async Task<IActionResult> IndexAsync(string error, string code, [FromQuery(Name = "state")] string returnUrl)
        {
            if (!string.IsNullOrEmpty(code) && string.IsNullOrEmpty(error))
            {
                var fullHostName = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}";

                var accessToken = await _googleOAuthService
                    .ExchangeAuthorizeCodeAsync(code, $"{fullHostName}/OAuth", _oAuthConfig.ClientId, _oAuthConfig.ClientSecret);

                if (!string.IsNullOrEmpty(accessToken))
                {
                    var userInfo = await _googleOAuthService.GetUserAsync(accessToken);
                    var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                    var sessionId = Guid.NewGuid();

                    var user = await _usersRepository.Read().Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email.Equals(userInfo.Email));
                    if (user != null)
                    {
                        var roles = user.Roles.Select(x => x.Name).ToHashSet();
                        var creatingTime = DateTime.UtcNow;
                        var appUser = new AppUser(roles, ip, user,creatingTime);
                        HttpContext.User = appUser;
                        _storage.Set(sessionId, appUser);
                        HttpContext.Response.Cookies.Append("sessionId", sessionId.ToString());

                        return Redirect(string.IsNullOrEmpty(returnUrl) ? "/Home/Index" : returnUrl);
                    }
                }
            }

            return new StatusCodeResult(403);
        }
    }
}
