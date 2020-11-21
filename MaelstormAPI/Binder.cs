using System;
using System.Collections.Generic;
using System.IO;
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
                // blazor doesn't allow to read configuration from file
                var config = new ConfigurationBuilder()
                        .AddInMemoryCollection(new Dictionary<string, string>
                        {
                            {"baseUrl", "http://localhost:5000"},
                            {"messageHub", "http://localhost:5000/messHub"}
                        })
                        .Build();
                return config;
            });

            Bind<IApi>().To<Api>().InSingletonScope();
            Bind<IAccountService>().To<AccountService>();
            Bind<ISessionService>().To<SessionService>();
            Bind<IDialogService>().To<DialogService>();
            Bind<IUserService>().To<UserService>();
            Bind<ISignalRService>().To<SignalRService>();
            Bind<ICryptographyService>().To<CryptographyService>();
        }
    }
}