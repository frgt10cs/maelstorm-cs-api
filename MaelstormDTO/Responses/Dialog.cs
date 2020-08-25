using Newtonsoft.Json;

namespace MaelstormDTO.Responses
{
    public class Dialog
    {
        [JsonProperty] public int Id { get; set; }
        [JsonProperty] public string SaltBase64 { get; set; }
        [JsonProperty] public string EncryptedKey { get; set; }
        [JsonProperty] public string InterlocutorNickname { get; set; }
        [JsonProperty] public string InterlocutorImage { get; set; }
        [JsonProperty] public int InterlocutorId { get; set; }
        [JsonProperty] public Message LastMessage { get; set; }
        [JsonProperty] public bool IsClosed { get; set; }

    }
}