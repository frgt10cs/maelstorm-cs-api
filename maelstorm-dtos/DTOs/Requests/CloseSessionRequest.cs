namespace maelstorm_dtos.DTOs.Requests
{
    public class CloseSessionRequest
    {
        public string SessionId { get; set; }
        public bool BanDevice { get; set; }
    }
}