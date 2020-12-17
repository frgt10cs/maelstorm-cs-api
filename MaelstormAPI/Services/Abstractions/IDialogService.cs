using System.Collections.Generic;
using System.Threading.Tasks;
using MaelstormApi.Models;

namespace MaelstormApi.Services.Abstractions
{
    public interface IDialogService
    {
        public Task<List<Dialog>> GetDialogsAsync(int offset = 0, int count = 10);
        public Task<Dialog> GetDialogAsync(long interlocutorId);
    }
}