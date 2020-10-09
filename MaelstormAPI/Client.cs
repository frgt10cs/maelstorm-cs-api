using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DeviceId;
using MaelstormApi.Models;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;
using Newtonsoft.Json;

namespace MaelstormApi
{
    public static class Client
    {
        private static readonly HttpClient HttpClient;
        private const int TokenExpiresInMinutes = 5;
        private static Tokens _tokens;
        internal static long Id;
        private static readonly string Fingerprint;
        private static readonly string App;
        private static readonly string Os;

        static Client()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("http://localhost:5000/api/");
            Fingerprint = new DeviceIdBuilder()
                .AddMachineName()
                .AddProcessorId()
                .AddMotherboardSerialNumber()
                .AddSystemDriveSerialNumber()
                .ToString();
            Os  =  System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            App = ".net";
        }

        public static bool IsAuthenticated => _tokens != null;
        
        /// <summary>
        /// Sends a request to the server
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data">Data which will be included into request</param>        
        /// <returns></returns>
        internal static async Task<ServerResponse> RequestAsync(HttpRequestMessage message, object data = null)
        {
            if (data != null)
            {
                string jsonData = JsonConvert.SerializeObject(data);
                message.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            }

            var response = await HttpClient.SendAsync(message);
            var serverResponse = await response.AsServerResponseAsync();
            return serverResponse;
        }
        
        /// <summary>
        /// Sends a request to the server including authentication tokens
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data">Data which will be included into request</param>
        /// <typeparam name="T">Expecting type of returning server data</typeparam>
        /// <returns></returns>
        internal static async Task<ServerResponse> AuthRequestAsync(HttpRequestMessage message, object data = null)
        {
            if (IsTokenExpired() && !await RefreshTokenAsync())
            {
                Logout();
                return new ServerResponse()
                {
                    ProblemDetails = new ProblemDetails("The session is expired. It can't be refreshed due to the invalid token.")
                };
            }

            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer",_tokens.AccessToken);
            if (data != null)
            {
                string json = JsonConvert.SerializeObject(data);
                message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            var response = await HttpClient.SendAsync(message);
            
            var serverResponse = await response.AsServerResponseAsync();

            return serverResponse;
        }        
        
        public static async Task<bool> AuthenticateAsync(string login, string password)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "authentication");
            var response = await RequestAsync(message,
                new AuthenticationRequest()
                {
                    Login = login,
                    Password = password,
                    Fingerprint = Fingerprint,
                    App = App,
                    Os = Os
                });

            if (response.Ok)
            {
                var authResultData = response.GetContent<AuthenticationResult>();
                _tokens = authResultData.Tokens;
                Id = authResultData.Id;
                return true;
            }

            return false;
        }

        public static async Task Logout()
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, "sessions/current");
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokens.AccessToken);
            await HttpClient.SendAsync(message);
            _tokens = null;
        }
        
        private static bool IsTokenExpired()
        {
            return (DateTime.Now - _tokens.GenerationTime).TotalMinutes > TokenExpiresInMinutes;
        }

        private static async Task<bool> RefreshTokenAsync()
        {
            var refreshTokenInfo = new RefreshTokenRequest()
            {
                AccessToken = _tokens.AccessToken,
                RefreshToken = _tokens.RefreshToken,
                Fingerprint = Fingerprint
            };
            var message = new HttpRequestMessage(HttpMethod.Post, "authentication/refresh");
            var result = await RequestAsync(message, refreshTokenInfo);
            if (result.Ok)
            {
                _tokens = result.GetContent<Tokens>();
                return true;
            }

            _tokens = null;
            return false;
        }
    }
}