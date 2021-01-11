using System;
using System.Collections.Generic;
using MaelstormApi.Services.Abstractions;
using MaelstormDTO.Responses;

namespace MaelstormApi.Services.Implementations
{
    public class MessageNotificationService : IMessageNotificationService
    {

        private List<IObserver<Message>> _subscribers;
        private ISignalRService _signalRService;
        public MessageNotificationService(ISignalRService signalRService)
        {
            _subscribers = new List<IObserver<Message>>();
            _signalRService = signalRService;
            _signalRService.RegisterHandler<Message>("OnNewMessage", message => _subscribers.ForEach(s => s.OnNext(message)));
        }
        public void SubscribeForNewMessages(IObserver<Message> subscriber)
        {
            _subscribers.Add(subscriber);
        }
    }
}