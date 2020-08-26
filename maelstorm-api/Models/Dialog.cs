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

        public async Task SendMessage(maelstorm_api.Models.Message dialogMessage)
        {
            dialogMessage.DialogId = dialog.Id;
            dialogMessage.AuthorId = Client.Id;
            dialogMessage.IVBase64 = "";
            
            var httpMessage = new HttpRequestMessage(HttpMethod.Post, "messages");
            var messageRequest = new SendMessageRequest()
            {
                DialogId =  dialogMessage.DialogId,
                Text = dialogMessage.Text,
                IVBase64 = dialogMessage.IVBase64
            };
            var result = await Client.AuthRequestAsync<RequestResult<DeliveredMessageInfo>>(httpMessage, messageRequest);
            if (result.Ok)
            {
                dialogMessage.Id = result.Data.MessageId;
                dialogMessage.DateOfSending = result.Data.DateOfSending;
            }
        }
    }
}