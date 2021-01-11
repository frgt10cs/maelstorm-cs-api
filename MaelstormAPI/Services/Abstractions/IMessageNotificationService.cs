using System;
using MaelstormDTO.Responses;

namespace MaelstormApi.Services.Abstractions
{
    public interface IMessageNotificationService
    {
        void SubscribeForNewMessages(IObserver<Message> observer);
    }
}