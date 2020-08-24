using Newtonsoft.Json;

namespace maelstorm_dtos.DTOs.Responses
{
    public class ClientSessions
    {
        [JsonProperty] public Session Session { get; set; }
        [JsonProperty] public SignalRSession SignalRSession { get; set; }
    }
}