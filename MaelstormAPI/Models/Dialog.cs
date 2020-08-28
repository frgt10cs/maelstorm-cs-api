using System.Net.Http;
using System.Threading.Tasks;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;

namespace maelstorm_api.Models
{
    public class Dialog
    {
        public MaelstormDTO.Responses.Dialog dialog;

        internal Dialog(MaelstormDTO.Responses.Dialog dialog)
        {
            this.dialog = dialog;
        }

        public async Task SendMessage(Message message)
        {
            message.DialogId = dialog.Id;
            message.AuthorId = Client.Id;
            message.IVBase64 = "";
            
            var httpMessage = new HttpRequestMessage(HttpMethod.Post, "messages");
            var messageRequest = new SendMessageRequest()
            {
                DialogId =  message.DialogId,
                Text = message.Text,
                IVBase64 = message.IVBase64
            };
            var result = await Client.AuthRequestAsync<RequestResult<DeliveredMessageInfo>>(httpMessage, messageRequest);
            if (result.Ok)
            {
                message.Id = result.Data.MessageId;
                message.DateOfSending = result.Data.DateOfSending;
            }
        }
    }
}