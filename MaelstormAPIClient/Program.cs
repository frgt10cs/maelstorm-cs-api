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
                var sessions = Sessions.GetSessionsAsync().Result;
                foreach (var session in sessions)
                {
                    Console.WriteLine(session.Session.SessionId);
                    Console.WriteLine(session.SignalRSession?.Ip);
                    Console.WriteLine("===");
                }
            }
            else
            {
                Console.WriteLine("nope");
            }
        }
    }
}