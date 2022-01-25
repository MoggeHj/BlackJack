using System;
using BlackJack.core;
using BlackJack.core.Games;
using BlackJack.core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IGame, EuropeanBlackJack>();
                    services.AddSingleton<IDeck, Deck>();
                    services.AddSingleton<IGameFactory, GameFactory>();
                }).Build();

            try
            {
                var service = host.Services.GetService<IGameFactory>().Create("EuropeanBlackJack");
                service.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to start the requested game");
                throw;
            }
            
        }

}
}
