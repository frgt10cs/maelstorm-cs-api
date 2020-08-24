using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DeviceId;
using maelstorm_dtos.DTOs.Requests;
using maelstorm_dtos.DTOs.Responses;
using Newtonsoft.Json;
using Ninject.Activation;

namespace maelstorm_api
{
    public static class Client
    {
        private static readonly HttpClient httpClient;
        private const int tokenExpiresInMinutes = 5;
        private static Tokens tokens;
        private static string fingerprint;
        private static string app;
        private static string os;

        static Client()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress =new Uri("http://localhost:5000/api/");fingerprint = new DeviceIdBuilder()
                .AddMachineName()
                .AddProcessorId()
                .AddMotherboardSerialNumber()
                .AddSystemDriveSerialNumber()
                .ToString();
            os  =  System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            app = ".net";
        }

        public static bool isAuthenticated => tokens != null;
        
        /// <summary>
        /// Sends a request to the server
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data">Data which will be included into request</param>
        /// <typeparam name="T">Expecting type of returning server data</typeparam>
        /// <returns></returns>
        internal static async Task<RequestResult<T>> RequestAsync<T>(HttpRequestMessage message, object data = null)
        {
            if (data != null)
            {
                string json = JsonConvert.SerializeObject(data);
                message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var response = await httpClient.SendAsync(message);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(json))
                {
                    var result = JsonConvert.DeserializeObject<RequestResult<T>>(json);
                    return result;
                }
            }

            return new RequestResult<T>()
            {
                Ok = false,
                ErrorMessages = new List<string>()
                {
                    response.ReasonPhrase
                }
            };
        }
        
        /// <summary>
        /// Sends a request to the server including authentication tokens
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data">Data which will be included into request</param>
        /// <typeparam name="T">Expecting type of returning server data</typeparam>
        /// <returns></returns>
        internal static async Task<T> AuthRequestAsync<T>(HttpRequestMessage message, object data = null)
        {
            if (IsTokenExpired() && !await RefreshTokenAsync())
            {
                Logout();
                return default(T);
            }

            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer",tokens.AccessToken);
            if (data != null)
            {
                string json = JsonConvert.SerializeObject(data);
                message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(message);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(json))
                {
                    var result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                }
            }

            return default(T);
        }
        
        public static async Task<bool> AuthenticateAsync(string login, string password)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "authentication");
            var result = await RequestAsync<AuthenticationResult>(message,
                new AuthenticationRequest()
                {
                    Login = login,
                    Password = password,
                    Fingerprint = fingerprint,
                    App = app,
                    Os = os
                });
            
            if (result.Ok)
            {
                tokens = result.Data.Tokens;
                return true;
            }

            return false;
        }

        public static async Task Logout()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "authenticate/logout");
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            await httpClient.SendAsync(message);
            tokens = null;
        }
        
        private static bool IsTokenExpired()
        {
            return (DateTime.Now - tokens.GenerationTime).TotalMinutes > tokenExpiresInMinutes;
        }

        private static async Task<bool> RefreshTokenAsync()
        {
            var refreshTokenInfo = new RefreshTokenRequest()
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                Fingerprint = fingerprint
            };
            var message = new HttpRequestMessage(HttpMethod.Post, "authentication/refresh");
            var result = await RequestAsync<Tokens>(message, refreshTokenInfo);
            if (result.Ok)
            {
                tokens = result.Data;
                return true;
            }

            tokens = null;
            return false;
        }
    }
}