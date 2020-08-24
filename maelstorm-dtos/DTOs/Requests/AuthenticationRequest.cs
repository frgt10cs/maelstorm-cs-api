using Newtonsoft.Json;

namespace maelstorm_dtos.DTOs.Requests
{
    public class AuthenticationRequest
    {
        [JsonProperty]
        public string Login { get; set; }
        [JsonProperty]
        public string Password { get; set; }
        [JsonProperty]
        public string App { get; set; }
        [JsonProperty]
        public string Os { get; set; }
        [JsonProperty]
        public string Fingerprint { get; set; }
    }
}