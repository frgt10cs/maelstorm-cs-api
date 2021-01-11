using System;
using System.Reflection;
using MaelstormApi.Services.Abstractions;
using MaelstormAPI.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Ninject;

namespace MaelstormApi
{
    public class ApiClient
    {
        private static ApiClient _instance;
        public static ApiClient Instance => _instance ??= new ApiClient();

        public IDialogService Dialogs { get; private set; }
        public IUserService Users { get; private set; }
        public ISessionService Sessions { get; private set; }
        public IAccountService Accounts { get; private set; }
        //public ISignalRService SignalR { get; private set; }
        public IMessageNotificationService MessageNotificationService { get; private set; }
        public IApi Api { get; private set; }

        public ApiClient()
        {
            var settings = new Ninject.NinjectSettings() { LoadExtensions = false };
            var kernel = new StandardKernel(settings, new Binder());
            // kernel.Load(Assembly.GetEntryAssembly());


            Dialogs = kernel.Get<IDialogService>();
            Users = kernel.Get<IUserService>();
            Sessions = kernel.Get<ISessionService>();
            Accounts = kernel.Get<IAccountService>();
            //SignalR = kernel.Get<SignalRService>();
            MessageNotificationService = kernel.Get<IMessageNotificationService>();
            Api = kernel.Get<IApi>();
        }
    }
}