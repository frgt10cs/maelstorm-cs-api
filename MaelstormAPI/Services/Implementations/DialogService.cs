using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Models;
using MaelstormApi.Services.Abstractions;
using MaelstormApi.Services.Implementations;

namespace MaelstormAPI.Services.Implementations
{
    public class DialogService : IDialogService
    {
        public IApi _api;

        public DialogService(IApi api)
        {
            this._api = api;
        }
        public async Task<List<Dialog>> GetDialogsAsync(int offset = 0, int count = 10)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"dialogs?offset={offset}&count={count}");
            var response = await _api.AuthRequestAsync(message);
            if (response.Ok)
            {
                var dialogs = response.GetContent<List<MaelstormDTO.Responses.Dialog>>();
                return dialogs.Select(d => new Dialog(d, _api)).ToList();
            }
            return new List<Dialog>();
        }

        public async Task<Dialog> GetDialogAsync(long dialogId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"dialogs/{dialogId}");
            var response = await _api.AuthRequestAsync(message);
            if (response.Ok)
            {
                var dialog = response.GetContent<MaelstormDTO.Responses.Dialog>();
                return new Dialog(dialog, _api);
            }

            return null;
        }

        public async Task<Dialog> GetDialogByInterlocutorIdAsync(long interlocutorId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"dialogs?interlocutorId={interlocutorId}");
            var response = await _api.AuthRequestAsync(message);
            if (response.Ok)
            {
                var dialog = response.GetContent<MaelstormDTO.Responses.Dialog>();
                return new Dialog(dialog, _api);
            }

            return null;
        }
    }
}