using Newtonsoft.Json;
using System;

namespace Tuto.Domain.Authorization
{
    public class UserInfo
    {
        public string Id { get; set; }

        public string Email { get; set; }

        [JsonProperty("given_name")]
        public string Name { get; set; }

        [JsonProperty("family_name")]
        public string Surname { get; set; }

        public Uri Picture { get; set; }
    }
}
