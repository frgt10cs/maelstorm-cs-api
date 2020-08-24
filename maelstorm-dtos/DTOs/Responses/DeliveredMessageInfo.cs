using System;

namespace maelstorm_dtos.DTOs.Responses
{
    public class DeliveredMessageInfo
    {
        public int MessageId { get; set; }
        public int BindId { get; set; }
        public DateTime SentAt { get; set; }
    }
}