using System.Collections.Generic;
using System.Threading.Tasks;
using MaelstormApi.Models;

namespace MaelstormApi.Services.Abstractions
{
    public interface IDialogService
    {
        Task<List<Dialog>> GetDialogsAsync(int offset = 0, int count = 10);
        Task<Dialog> GetDialogAsync(long dialogId);
        Task<Dialog> GetDialogByInterlocutorIdAsync(long interlocutorId);
    }
}