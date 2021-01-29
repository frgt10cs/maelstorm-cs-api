using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Services.Abstractions;
using MaelstormApi.Services.Implementations;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;
using Ninject.Infrastructure.Language;

namespace MaelstormApi.Models
{
    public class Dialog
    {
        private IApi _api;
        public MaelstormDTO.Responses.Dialog dialog;
        private List<Message> _messages;
        public IReadOnlyList<Message> Messages
        {
            get => _messages.AsReadOnly();
        }

        public void AddNewMessage(MaelstormDTO.Responses.Message message)
        {
            _messages.Add((Message)message);
        }

        public void RemoveMessage(Message message)
        {
            _messages.Remove(message);
        }

        public void RemoveMessage(long messageId)
        {
            var message = _messages.FirstOrDefault(m => m.Id == messageId);
            if (message != null)
                RemoveMessage(message);
        }

        internal Dialog(MaelstormDTO.Responses.Dialog dialog, IApi api)
        {
            this.dialog = dialog;
            this._api = api;
            _messages = new List<Message>();
        }

        public async Task SendMessageAsync(string text)
        {
            // check for valid

            var message = new Message()
            {
                DialogId = dialog.Id,
                Text = text,
            };

            var httpMessage = new HttpRequestMessage(HttpMethod.Post, "messages");
            var messageRequest = new SendMessageRequest()
            {
                DialogId = dialog.Id,
                Text = text,
                IVBase64 = ""
            };
            var response = await _api.AuthRequestAsync(httpMessage, messageRequest);
            if (response.Ok)
            {
                var deliveredMessageInfo = response.GetContent<DeliveredMessageInfo>();
                message.Id = deliveredMessageInfo.MessageId;
                message.DateOfSending = deliveredMessageInfo.DateOfSending;
            }
        }

        public async Task UploadMessagesAsync(bool unreaded, int offset, int count)
        {
            // check

            var type = unreaded ? "unreaded" : "readed";
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, $"messages/{type}?dialogId={dialog.Id}&offset={offset}&count={count}");
            var response = await _api.AuthRequestAsync(httpMessage);
            if (response.Ok)
            {
                var messageResponses = response.GetContent<IEnumerable<MaelstormDTO.Responses.Message>>();
                var messages = messageResponses.Select(mr => (Message)mr).ToList();
                _messages.AddRange(messages);
            }
        }
    }
}