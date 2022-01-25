using System.Collections.Generic;
using BlackJack.core.Interfaces;

namespace BlackJack.core.Tests
{
    public class TestDataProvider
    {
        public List<IPlayer> GetPlayers()
        {
            var players = new List<IPlayer>();
            players.Add(new Player()
            {
                Name = "Morgan",
                Hand = new List<Card>(),
                Status = Status.InGame
            });
            
            players.Add(new Player()
            {
                Name = "Erik",
                Hand = new List<Card>(),
                Status = Status.InGame
            });

            return players;
        }

        public List<IPlayer> GetDealer()
        {
            var dealer = new List<IPlayer>();
            dealer.Add(new Player()
            {
                Name = "Dealer",
                Hand = new List<Card>(),
                Status = Status.InGame
            });


            return dealer;
        }

        public IDeck GetDeck() => new Deck();

        public List<Card> GetHand(bool above21)
        {
            if (above21)
            {
                return new List<Card>()
                {
                    new Card()
                    {
                        Rank = 10,
                        Suit = Suit.Clubs
                    },
                    new Card()
                    {
                        Rank = 10,
                        Suit = Suit.Diamonds
                    },
                    new Card()
                    {
                        Rank = 5,
                        Suit = Suit.Hearts
                    }
                };
            }

            else
                return new List<Card>()
                {
                    new Card()
                    {
                        Rank = 10,
                        Suit = Suit.Clubs
                    }
                };
        }

        public List<Card> GetHandWithAces(int numberOfAces)
        {
           var hand = new List<Card>();
           for (int i = 0; i < numberOfAces; i++)
           {
                hand.Add(new Card()
                {
                    Rank = 1,
                    Suit = Suit.Clubs
                });
           }

           return hand;
        }
    }
}