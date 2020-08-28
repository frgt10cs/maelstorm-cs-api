using System;
using Newtonsoft.Json;

namespace MaelstormDTO.Responses
{
    public class Message
    {
        [JsonProperty]
        public long Id { get; set; }
        [JsonProperty]
        public string IVBase64 { get; set; }
        [JsonProperty]
        public long AuthorId { get; set; }
        [JsonProperty]
        public long DialogId { get; set; }
        [JsonProperty]
        public DateTime DateOfSending { get; set; }
        [JsonProperty]
        public bool IsReaded { get; set; }
        [JsonProperty]
        public string Text { get; set; }
    }
}