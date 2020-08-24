using System;
using Newtonsoft.Json;

namespace maelstorm_api.DTOs.Response
{
    public class Message
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string IVBase64 { get; set; }
        [JsonProperty]
        public int AuthorId { get; set; }
        [JsonProperty]
        public int DialogId { get; set; }
        [JsonProperty]
        public DateTime DateOfSending { get; set; }
        [JsonProperty]
        public byte Status { get; set; }
        [JsonProperty]
        public string Text { get; set; }
    }
}