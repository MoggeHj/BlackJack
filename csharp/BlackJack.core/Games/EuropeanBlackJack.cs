using System;
using System.Collections.Generic;
using BlackJack.core.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlackJack.core.Games
{
    public class EuropeanBlackJack :BaseBlackJack, IGame
    {
        private readonly ILogger _logger;
        public string Name { get; }

        public EuropeanBlackJack(ILogger<EuropeanBlackJack> logger, IDeck deck)
            : base(deck, 21)
        {
            _logger = logger;
            Name = "EuropeanBlackJack";
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void Start()
        {
            RegisterPlayers();
            _logger.LogInformation("Game has started");
            DealerPlay();
            PlayerPlay();
            DealerPlay();

            foreach (var player in Players)
            {
                var winner = GetWinner(Dealer, player);
                Console.WriteLine("Player {0} have hand {1} and the dealer have hand {2}", player.Name, CalculatePlayerHand(player), CalculatePlayerHand(Dealer));
                Console.WriteLine("The winner is {0}", winner.Name);
            }
            

        }

        /// <summary>
        /// Players play until they stand or bust
        /// </summary>
        private void PlayerPlay()
        {
            while (Players.Exists(x => x.Status == Status.InGame))
            {
                foreach (var player in Players)
                {
                    if (player.Status == Status.InGame)
                    {
                        Console.WriteLine("Player {0}: Stand or Hit", player.Name);
                        string read = Console.ReadLine()?.ToLower();
                        if (read == "hit")
                        {
                            var card = Deck.GiveCard();
                            player.Hand.Add(card);
                            Console.WriteLine("Player {0}: Hit with {1} {2}. Total is {3}", player.Name, card.Suit, card.GetRank(), CalculatePlayerHand(player));
                            if (Bust(player))
                            {
                                Console.WriteLine("Player {0}'s hand is above {1} and will not get more cards", player.Name, BustLimit);
                            }
                        }
                        else if (read == "stand")
                        {
                            player.Status = Status.Hold;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Dealer takes one card.
        /// If dealer has more then one card the dealer plays until dealer's hand reaches 17.
        /// </summary>
        private void DealerPlay()
        {
            if (Dealer.Hand.Count < 1)
            {
                var card = Deck.GiveCard();
                Dealer.Hand.Add(card);
                Console.WriteLine("Dealer hit with {0} {1}. Total is {2}", card.Suit, card.GetRank(), CalculatePlayerHand(Dealer));
            }
            else
            {
                while (CalculatePlayerHand(Dealer) <= 17)
                {
                    var card = Deck.GiveCard();
                    Dealer.Hand.Add(card);
                    Console.WriteLine("Dealer hit with {0} {1}. Total is {2}", card.Suit, card.GetRank(), CalculatePlayerHand(Dealer));
                }
            }
            
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }


        private void RegisterPlayers()
        {
            Console.WriteLine("How many players do you want to register:");
            var numberOfPlayers = int.Parse(Console.ReadLine());
            var players = new List<IPlayer>();
            for(int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine("Name of new player: ");
                var name = Console.ReadLine();
                players.Add(new Player()
                {
                    Name = name,
                    Hand = new List<Card>(),
                    Status = Status.InGame
                });
            }
            RegisterPlayers(players);
        }

    }
}