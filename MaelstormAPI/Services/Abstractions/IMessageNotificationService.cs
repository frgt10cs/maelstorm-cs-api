using System;
using MaelstormDTO.Responses;

namespace MaelstormApi.Services.Abstractions
{
    public interface IMessageNotificationService
    {        
        event Action<Message> OnNewMessage;
    }
}