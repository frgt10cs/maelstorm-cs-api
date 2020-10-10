using System.Threading.Tasks;
using MaelstormApi.Models;
using MaelstormDTO.Requests;

namespace MaelstormApi.Services.Abstractions
{
    public interface IAccountService
    {
        public Task<ServerResponse> RegistrationAsync(RegistrationRequest registrationRequest);
    }
}