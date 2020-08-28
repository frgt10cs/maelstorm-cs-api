using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dialog = maelstorm_api.Models.Dialog;

namespace maelstorm_api
{
    public static class Dialogs
    {
        public static async Task<List<Dialog>> GetDialogsAsync(int offset = 0, int count = 10)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"dialogs?offset={offset}&count={count}");
            var dialogs = await Client.AuthRequestAsync<List<MaelstormDTO.Responses.Dialog>>(message);
            return dialogs.Select(d => new Dialog(d)).ToList();
        } 
        
        public static async Task<Dialog> GetDialogAsync(int interlocutorId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"dialogs/{interlocutorId}");
            var dialog = await Client.AuthRequestAsync<MaelstormDTO.Responses.Dialog>(message);
            return new Dialog(dialog);
        }
    }
}