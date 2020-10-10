using System;
using System.Threading.Tasks;
using MaelstormApi;
using MaelstormApi.Services.Abstractions;
using Microsoft.AspNetCore.SignalR.Client;

namespace MaelstormAPI.Services.Implementations
{
    public class SignalRService : ISignalRService
    {
        private readonly HubConnection _connection;

        public bool IsAuthenticated { get; private set; }

        public SignalRService()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(Configuration.Config["messageHub"])
                .Build();

            _connection.On("OnHubAuthSuccess", () =>
            {
                IsAuthenticated = true;
            });
            
            _connection.On<string>("OnHubAuthFailed", (errorMessage) =>
            {
                IsAuthenticated = false;
                Console.WriteLine(errorMessage);
            });
        }

        public async Task<bool> Connect()
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await _connection.StartAsync();
                return true;
            }

            return false;
        }

        public void Disconnect()
        {
            if (_connection != null && _connection.State == HubConnectionState.Connected)
            {
                _connection.StopAsync();
            }
        }
        
        public async Task AuthAsync(string token, string fingerprint)
        {
            await _connection.InvokeAsync("auth", token, fingerprint);
        }
    }
}