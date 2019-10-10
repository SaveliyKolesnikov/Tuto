using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tuto.Domain.Authorization;
using Tuto.Services.Interfaces;

namespace Tuto.Services
{
    public class GoogleOAuthService : IGoogleOAuthService
    {
        private readonly IHttpClientFactory _clientFactory;

        public GoogleOAuthService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> ExchangeAuthorizeCodeAsync(string code, string redirectUrl, string clientId, string clientSecret)
        {
            var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret ),
                new KeyValuePair<string, string>("redirect_uri", redirectUrl),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            });

            var client = _clientFactory.CreateClient();
            var response = await client.PostAsync("https://accounts.google.com/o/oauth2/token", content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var token = await JsonSerializer.DeserializeAsync<Token>(await response.Content.ReadAsStreamAsync());

            return token?.access_token;
        }

        public async Task<UserInfo> GetUserAsync(string accessToken)
        {
            var userInfoResponse = await GetUserInfoResponseAsync(accessToken);
            return await JsonSerializer.DeserializeAsync<UserInfo>(await userInfoResponse.Content.ReadAsStreamAsync());
        }

        private Task<HttpResponseMessage> GetUserInfoResponseAsync(string accessToken)
        {
            var requestString = $"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={accessToken}";
            var client = _clientFactory.CreateClient();
            return client.GetAsync(requestString);
        }
    }
}
