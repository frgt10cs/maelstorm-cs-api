﻿namespace MaelstormDTO.Requests
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Fingerprint { get; set; }
    }
}