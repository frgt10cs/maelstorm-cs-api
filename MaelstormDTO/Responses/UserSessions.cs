using Newtonsoft.Json;

namespace MaelstormDTO.Responses
{
    public class UserSessions
    {
        [JsonProperty] public Session Session { get; set; }
        [JsonProperty] public SignalRSession SignalRSession { get; set; }
    }
}