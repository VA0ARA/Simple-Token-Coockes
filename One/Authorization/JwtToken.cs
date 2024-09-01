using Newtonsoft.Json;

namespace WebApp_UnderTheHood.Authorization
{
    public class JwtToken
    {
        //This attribute allows you to customize how individual properties of
        //a class are mapped to JSON keys and how JSON keys are mapped back to
        //properties during deserialization.
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; } 
    }
}
