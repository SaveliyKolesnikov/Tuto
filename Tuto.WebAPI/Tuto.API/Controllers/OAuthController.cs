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
using AutoMapper;
using Tuto.API.Authorization;

namespace Tuto.API.Controllers
{
    public class OAuthController : ControllerBase
    {
        private readonly ISessionStorage<AppUser> _storage;
        private readonly OAuthConfig _oAuthConfig;
        private readonly IRepository<User> _usersRepository;
        private readonly IGoogleOAuthService _googleOAuthService;
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;

        public OAuthController(ISessionStorage<AppUser> storage,
            IRepository<User> repository,
            IGoogleOAuthService googleOAuthService,
            IOptions<OAuthConfig> appSettings,
            IAppUserManager appUserManager,
            IMapper mapper)
        {
            _storage = storage;
            _usersRepository = repository;
            _googleOAuthService = googleOAuthService;
            _appUserManager = appUserManager;
            _mapper = mapper;
            _oAuthConfig = appSettings.Value;
        }

        [AuthFilter]
        [HttpGet]
        [Route("OAuth/GetCurrentUserId")]
        public Guid? GetCurrentUserIdAsync()
        {
            if (_appUserManager.TryGetUserId(Request.HttpContext.User, out var userId))
            {
                return userId;
            }

            return null;
        }

        [HttpPost]
        [Route("OAuth/LogOut")]
        public IActionResult LogOut()
        {
            var cookie = Request.HttpContext.Request.Cookies["sessionId"];

            if (!string.IsNullOrEmpty(cookie))
            {
                var sessionId = Guid.Parse(cookie);
                if (_storage.TryRemove(sessionId))
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("OAuth/Authorize")]
        public IActionResult Authorize([FromQuery] string returnUrl)
        {
            var fullHostName = $"{Request.HttpContext.Request.Scheme}://{Request.HttpContext.Request.Host.Value}";
            var authorizationUrl = GoogleOAuthHelpers.GetAuthorizationUrl(returnUrl, $"{fullHostName}/OAuth", _oAuthConfig.ClientId);
            return Redirect(authorizationUrl);
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

                    var user = await _usersRepository.Read().Include(u => u.Roles)
                        .FirstOrDefaultAsync(u => u.Email.Equals(userInfo.Email)) ?? await RegisterUserAsync(userInfo);

                    var roles = user.Roles?.Select(x => x.Name).ToHashSet() ?? Enumerable.Empty<string>().ToHashSet();
                    var creatingTime = DateTime.UtcNow;
                    var appUser = new AppUser(roles, ip, user, creatingTime);
                    HttpContext.User = appUser;
                    _storage.Set(sessionId, appUser);
                    HttpContext.Response.Cookies.Append("sessionId", sessionId.ToString());

                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/Home/Index" : returnUrl);
                }
            }

            return new StatusCodeResult(403);
        }

        private async Task<User> RegisterUserAsync(UserInfo userInfo)
        {
            var user = _mapper.Map<UserInfo, User>(userInfo);
            user.Roles = Array.Empty<Role>();
            _usersRepository.Create(user);
            await _usersRepository.Commit();
            return user;
        }
    }
}
