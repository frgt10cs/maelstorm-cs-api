using Microsoft.Extensions.Configuration;

namespace MaelstormApi
{
    public static class Configuration
    {
        public static IConfiguration Config;
        static Configuration()
        {
            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }
    }
}