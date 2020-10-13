using MaelstormApi.Models;
using MaelstormApi.Services.Abstractions;
using MaelstormApi.Services.Implementations;
using MaelstormAPI.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;

namespace MaelstormApi
{
    public class Binder:NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().ToMethod(ctx =>
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                return config;
            });

            Bind<IApi>().To<Api>().InSingletonScope();
            Bind<IAccountService>().To<AccountService>();
            Bind<ISessionService>().To<SessionService>();
            Bind<IDialogService>().To<DialogService>();
            Bind<IUserService>().To<UserService>();
            Bind<ISignalRService>().To<SignalRService>();
        }
    }
}