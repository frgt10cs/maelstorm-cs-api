using System;
using MaelstormApi;

namespace MaelstormApiClient
{
    internal abstract class Command
    {
        public string Name { get; protected set; }
        internal abstract void Excecute();
    }

    internal class AuthenticationCommand : Command
    {
        public AuthenticationCommand()
        {
            Name = "Authenticate";
        }
        internal override void Excecute()
        {
            string login;
            Console.Write("Login: ");
            login = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            var api = ApiClient.Instance;
            var result = api.Api.AuthenticateAsync(login, password).Result;
            Console.WriteLine(result?"OK!":"Invalid login or password");
        }
    }

    class Program
    {
        private static Command[] commands = {
            new AuthenticationCommand(),
        };
        
        static void Main(string[] args)
        {
            Console.WriteLine("===== Welcome to Maelstorm =====");
            Console.WriteLine("Select any action:");
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"[{i}]: {commands[i].Name}");
            }
        }
    }
}