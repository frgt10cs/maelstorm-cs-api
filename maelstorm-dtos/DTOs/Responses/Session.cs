﻿using System;

namespace maelstorm_dtos.DTOs.Responses
{
    public class Session
    {
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