using System;
using System.Text;

namespace Tuto.API.Authorization
{
    public static class GoogleOAuthHelpers
    {
        private const string OAuthURl = "https://accounts.google.com/o/oauth2/auth?";
        private const string OAuthScopes = "https://www.googleapis.com/auth/userinfo.email";
        private static string EncodedScopes { get; } = Uri.EscapeDataString(OAuthScopes);
        private static StringBuilder StringBuilderInstance { get; } = new StringBuilder();

        public static string GetAuthorizationUrl(string returnUrl, string redirectUrl, string clientId)
        {
            var encodedReturnUrl = string.IsNullOrEmpty(returnUrl) ? null : Uri.EscapeDataString(returnUrl);
            var encodedRedirectUrl = Uri.EscapeDataString(redirectUrl);

            return StringBuilderInstance.Clear()
                                        .Append(OAuthURl)
                                        .Append("client_id=").Append(clientId)
                                        .Append("&redirect_uri=").Append(encodedRedirectUrl)
                                        .Append("&response_type=code")
                                        .Append("&scope=").Append(EncodedScopes)
                                        .Append("&access_type=offline")
                                        .Append("&state=").Append(encodedReturnUrl)
                                        .ToString();
        }
    }
}
