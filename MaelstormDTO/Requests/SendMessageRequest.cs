using maelstorm_dtos.Validation;

namespace MaelstormDTO.Requests
{
    public class SendMessageRequest
    {
        public long DialogId { get; set; }
        [MessageText]
        public string Text { get; set; }
        
        public string IVBase64 { get; set; }
    }
}