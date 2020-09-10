using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Models;

namespace MaelstormApi
{
    public static class Dialogs
    {
        public static async Task<List<Dialog>> GetDialogsAsync(int offset = 0, int count = 10)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"dialogs?offset={offset}&count={count}");
            var response = await Client.AuthRequestAsync(message);
            if (response.Ok)
            {
                var dialogs = response.GetContent<List<MaelstormDTO.Responses.Dialog>>();
                return dialogs.Select(d => new Dialog(d)).ToList();
            }
            return new List<Dialog>();
        } 
        
        public static async Task<Dialog> GetDialogAsync(int interlocutorId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"dialogs/{interlocutorId}");
            var response = await Client.AuthRequestAsync(message);
            if (response.Ok)
            {
                var dialog = response.GetContent<MaelstormDTO.Responses.Dialog>();
                return new Dialog(dialog);   
            }

            return null;
        }
    }
}