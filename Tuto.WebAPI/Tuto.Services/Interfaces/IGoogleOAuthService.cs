using System.Threading.Tasks;
using Tuto.Domain.Authorization;

namespace Tuto.Services.Interfaces
{
    public interface IGoogleOAuthService
    {
        /// <returns>Access token</returns>
        Task<string> ExchangeAuthorizeCodeAsync(string code, string redirectUrl, string clientId, string clientSecret);

        Task<UserInfo> GetUserAsync(string accessToken);
    }
}
