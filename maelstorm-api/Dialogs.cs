using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using maelstorm_api.DTOs.Response;
using maelstorm_dtos.DTOs.Responses;

namespace maelstorm_api
{
    public static class Dialogs
    {
        public static async Task<List<Dialog>> GetDialogsAsync(int offset = 0, int count = 10)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"dialogs?offset={offset}&count={count}");
            var result = await Client.AuthRequestAsync<List<Dialog>>(message);
            return result;
        } 
        
        public static async Task<Dialog> GetDialogAsync(int interlocutorId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"dialogs/{interlocutorId}");
            var result = await Client.AuthRequestAsync<Dialog>(message);
            return result;
        }
    }
}