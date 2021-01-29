using System;
using System.Collections.Generic;
using MaelstormApi.Services.Abstractions;
using MaelstormDTO.Responses;

namespace MaelstormApi.Services.Implementations
{
    public class MessageNotificationService : IMessageNotificationService
    {

        public event Action<Message> OnNewMessage;
        private ISignalRService _signalRService;
        public MessageNotificationService(ISignalRService signalRService)
        {
            _signalRService = signalRService;
            _signalRService.RegisterHandler<Message>("OnNewMessage", message => OnNewMessage?.Invoke(message));
        }
    }
}