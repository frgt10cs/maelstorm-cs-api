namespace MaelstormDTO.Requests
{
    public class SendMessageRequest
    {
        public string Text { get; set; }
        
        public string IVBase64 { get; set; }
    }
}