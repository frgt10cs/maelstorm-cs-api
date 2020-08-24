using System;
using System.Threading.Tasks;
using maelstorm_api;

namespace maelstorm_api_client
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Client.AuthenticateAsync("huii", "1234567890").Result;
            if (result)
            {
                Console.WriteLine("Token generation time: " + DateTime.Now);
                Task.Delay(305000).Wait();
                Console.WriteLine("Run!");
                Console.Beep();
                Console.Beep();
                Console.Beep();
                var dialogs = Dialogs.GetDialogsAsync().Result;
                foreach (var dialog in dialogs)
                {
                    Console.WriteLine(dialog.EncryptedKey);
                }
            }
            else
            {
                Console.WriteLine("nope");
            }
        }
    }
}