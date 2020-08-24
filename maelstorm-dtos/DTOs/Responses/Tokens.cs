using System;
using Newtonsoft.Json;

namespace maelstorm_dtos.DTOs.Responses
{
    public class Tokens
    {
        [JsonProperty]
        public string AccessToken { get; set; }
        [JsonProperty]
        public DateTime GenerationTime { get; set; }
        [JsonProperty]
        public string RefreshToken { get; set; }
    }
}