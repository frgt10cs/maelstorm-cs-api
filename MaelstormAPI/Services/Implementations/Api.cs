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
using MaelstormApi.Services.Abstractions;
using MaelstormDTO.Requests;
using MaelstormDTO.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace MaelstormApi.Services.Implementations
{
    public class Api : IApi
    {
        private readonly HttpClient HttpClient;
        private const int TokenExpiresInMinutes = 5;
        private Tokens _tokens;
        internal long Id;
        private byte[] userPrivateKey;
        private readonly string _fingerprint;
        private readonly string _app;
        private readonly string _os;
        private ICryptographyService _cryptographyService;
        private ISignalRService _signalRService;

        public Api(IConfiguration configuration, ICryptographyService cryptographyService,
            ISignalRService signalRService)
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri($"{configuration["baseUrl"]}/api/");
            _fingerprint = new DeviceIdBuilder()
                .AddMachineName()
                .AddProcessorId()
                .AddMotherboardSerialNumber()
                .AddSystemDriveSerialNumber()
                .ToString();
            _os = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            _app = ".net";
            _cryptographyService = cryptographyService;
            _signalRService = signalRService;
        }

        public bool IsAuthenticated => _tokens != null;

        /// <summary>
        /// Sends a request to the server
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data">Data which will be included into request</param>        
        /// <returns></returns>
        public async Task<ServerResponse> RequestAsync(HttpRequestMessage message, object data = null)
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
        public async Task<ServerResponse> AuthRequestAsync(HttpRequestMessage message, object data = null)
        {
            if (IsTokenExpired() && !await RefreshTokenAsync())
            {
                LogoutAsync();
                return new ServerResponse()
                {
                    ProblemDetails = new ProblemDetails("The session is expired. It can't be refreshed due to the invalid token.")
                };
            }

            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokens.AccessToken);
            if (data != null)
            {
                string json = JsonConvert.SerializeObject(data);
                message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            var response = await HttpClient.SendAsync(message);

            var serverResponse = await response.AsServerResponseAsync();

            return serverResponse;
        }

        public async Task<bool> AuthenticateAsync(string login, string password)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "authentication");
            var response = await RequestAsync(message,
                new AuthenticationRequest()
                {
                    Login = login,
                    Password = password,
                    Fingerprint = _fingerprint,
                    App = _app,
                    Os = _os
                });

            if (response.Ok)
            {
                var authResultData = response.GetContent<AuthenticationResult>();
                _tokens = authResultData.Tokens;
                Id = authResultData.Id;
                var userAesKey = _cryptographyService.Pbkdf2(password, Convert.FromBase64String(authResultData.KeySaltBase64));
                userPrivateKey = _cryptographyService.AesDecryptBytes(Convert.FromBase64String(authResultData.EncryptedPrivateKey),
                     userAesKey, Convert.FromBase64String(authResultData.IVBase64), 256);
                return true;
            }

            return false;
        }

        public async Task<bool> EstablishSignalRConnection()
        {
            /// TODO: check for success
            await _signalRService.AuthAsync(_tokens.AccessToken, _fingerprint);
            return true;
        }

        public async Task LogoutAsync()
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, "sessions/current");
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokens.AccessToken);
            await HttpClient.SendAsync(message);
            _tokens = null;
        }

        private bool IsTokenExpired()
        {
            return (DateTime.Now - _tokens.GenerationTime).TotalMinutes > TokenExpiresInMinutes;
        }

        private async Task<bool> RefreshTokenAsync()
        {
            var refreshTokenInfo = new RefreshTokenRequest()
            {
                AccessToken = _tokens.AccessToken,
                RefreshToken = _tokens.RefreshToken,
                Fingerprint = _fingerprint
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