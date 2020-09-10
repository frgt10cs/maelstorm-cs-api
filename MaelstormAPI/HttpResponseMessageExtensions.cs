using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MaelstormApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaelstormApi
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<ServerResponse> AsServerResponseAsync(this HttpResponseMessage message)
        {
            ServerResponse response = new ServerResponse();
            var messageContent = await message.Content.ReadAsStringAsync();
            switch (message.StatusCode)
            {
                case HttpStatusCode.OK:
                    response.Content = messageContent;
                    break;
                case HttpStatusCode.BadRequest:
                    var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(messageContent);
                    response.ProblemDetails = problemDetails;
                    break;
            }

            return response;
        }
    }
}