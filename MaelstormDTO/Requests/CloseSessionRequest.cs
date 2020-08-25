namespace MaelstormDTO.Requests
{
    public class CloseSessionRequest
    {
        public string SessionId { get; set; }
        public bool BanDevice { get; set; }
    }
}