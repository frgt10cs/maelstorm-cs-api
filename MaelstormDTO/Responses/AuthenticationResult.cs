using Newtonsoft.Json;

namespace MaelstormDTO.Responses
{
    public class AuthenticationResult
    {
        [JsonProperty]
        public long Id {get; set; }
        [JsonProperty]
        public string IVBase64 { get; set; }
        [JsonProperty]
        public string KeySaltBase64 { get; set; }
        [JsonProperty]
        public string PublicKey { get; set; }
        [JsonProperty]
        public string EncryptedPrivateKey { get; set; }
        [JsonProperty]
        public Tokens Tokens { get; set; }
    }
}