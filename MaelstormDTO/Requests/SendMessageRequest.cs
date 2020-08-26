namespace MaelstormDTO.Requests
{
    public class SendMessageRequest
    {
        public int DialogId { get; set; }
        public string Text { get; set; }
        
        public string IVBase64 { get; set; }
    }
}