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
        public IApi Api { get; private set; }

        public ApiClient()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            
            Dialogs = kernel.Get<IDialogService>();
            Users = kernel.Get<IUserService>();
            Sessions = kernel.Get<ISessionService>();
            Accounts = kernel.Get<IAccountService>();
            Api = kernel.Get<IApi>();
        }
    }
}