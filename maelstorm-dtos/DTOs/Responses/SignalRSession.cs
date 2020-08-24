using System;

namespace maelstorm_dtos.DTOs.Responses
{
    public class SignalRSession
    {
        public string SessionId;
        public string ConnectionId;
        public string Fingerprint;
        public string Ip;
        public DateTime StartedAt;
    }
}