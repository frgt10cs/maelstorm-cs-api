using System;

namespace maelstorm_api.Models
{
    public class Message
    {
        public int Id { get; internal set; }
        public string Text { get; set; }
        internal string IVBase64;
        public DateTime DateOfSending { get; internal set; }
        public int DialogId { get; internal set; }
        public int AuthorId { get; internal set; }
    }
}