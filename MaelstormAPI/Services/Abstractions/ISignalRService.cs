using System.Threading.Tasks;

namespace MaelstormApi.Services.Abstractions
{
    public interface ISignalRService
    {
        bool IsAuthenticated { get; }
        public Task<bool> Connect();
        public void Disconnect();
        Task AuthAsync(string token, string fingerprint);
    }
}