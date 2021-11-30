using Newtonsoft.Json;

namespace BasiqTestApp.Model
{
    public class Token
    {
        [JsonProperty]
        public string access_token { get; set; }
        [JsonProperty]
        public string token_type { get; set; }
    }
}
