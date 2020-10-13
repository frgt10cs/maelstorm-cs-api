using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Services.Implementations;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;

namespace MaelstormApi.Models
{
    public class Dialog
    {
        private Api _api;
        public MaelstormDTO.Responses.Dialog dialog;

        internal Dialog(MaelstormDTO.Responses.Dialog dialog, Api api)
        {
            this.dialog = dialog;
            this._api = api;
        }

        public async Task SendMessage(Message message)
        {
            message.DialogId = dialog.Id;
            message.AuthorId = _api.Id;
            message.IVBase64 = "";
            
            var httpMessage = new HttpRequestMessage(HttpMethod.Post, "messages");
            var messageRequest = new SendMessageRequest()
            {
                DialogId =  message.DialogId,
                Text = message.Text,
                IVBase64 = message.IVBase64
            };
            var response = await _api.AuthRequestAsync(httpMessage, messageRequest);
            if (response.Ok)
            {
                var deliveredMessageInfo = response.GetContent<DeliveredMessageInfo>();
                message.Id = deliveredMessageInfo.MessageId;
                message.DateOfSending = deliveredMessageInfo.DateOfSending;
            }
        }
    }
}