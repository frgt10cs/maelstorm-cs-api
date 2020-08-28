using System;

namespace maelstorm_api.Models
{
    public class Message
    {
        public long Id { get; internal set; }
        public string Text { get; set; }
        internal string IVBase64;
        public DateTime DateOfSending { get; internal set; }
        public long DialogId { get; internal set; }
        public long AuthorId { get; internal set; }
    }
}