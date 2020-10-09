using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace MaelstormApi
{
    public static class SignalRClient
    {
        private static HubConnection _connection;

        public static bool IsAuthenticated { get; private set; }

        static SignalRClient()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("")
                .Build();
            
            _connection.On<string>("OnHubAuthFailed", (errorMessage) =>
            {
                Console.WriteLine(errorMessage);
            });
        }

        public static async Task<bool> Connect()
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await _connection.StartAsync();
                return true;
            }

            return false;
        }

        public static void Disconnect()
        {
            if (_connection != null && _connection.State == HubConnectionState.Connected)
            {
                _connection.StopAsync();
            }
        }
        
        internal static async Task AuthAsync(string token, string fingerprint)
        {
            await _connection.InvokeAsync("auth", token, fingerprint);
        }
    }
}