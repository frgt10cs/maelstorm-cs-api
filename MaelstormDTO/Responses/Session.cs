using System;

namespace MaelstormDTO.Responses
{
    public class Session
    {
        public long UserId { get; set; }
        public string SessionId { get; set; }
        public string RefreshToken { get; set; }
        public string OsCpu { get; set; }
        public string App { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }
        public string FingerPrint { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}