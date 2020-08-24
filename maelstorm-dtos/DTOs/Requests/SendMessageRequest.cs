namespace maelstorm_dtos.DTOs.Requests
{
    public class SendMessageRequest
    {
        public int InterlocutorId { get; set; }
        
        public int BindId { get; set; }        
        
        public string Text { get; set; }
        
        public string IVBase64 { get; set; }
        
        public int ReplyId { get; set; }       
    }
}