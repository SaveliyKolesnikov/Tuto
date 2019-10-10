using System.Text.Json.Serialization;

namespace Tuto.Domain.Authorization
{
    public class UserInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
