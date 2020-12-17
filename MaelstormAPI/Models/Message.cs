using System;

namespace MaelstormApi.Models
{
    public class Message
    {
        public long Id { get; internal set; }
        public string Text { get; set; }
        public DateTime DateOfSending { get; internal set; }
        public long DialogId { get; internal set; }
        public long AuthorId { get; internal set; }

        public static explicit operator Message(MaelstormDTO.Responses.Message message)
        {
            return new Message()
            {
                Id = message.Id,
                AuthorId = message.AuthorId,
                DialogId = message.DialogId,
                DateOfSending = message.DateOfSending,
                Text = message.Text
            };
        }
    }
}