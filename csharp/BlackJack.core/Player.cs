using System.Collections.Generic;
using BlackJack.core.Interfaces;

namespace BlackJack.core
{
    public enum Status
    {
        InGame,
        Hold,
        Lost
    }

    public class Player :IPlayer
    {
        public Player()
        {
            Hand = new List<Card>();
        }
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public Status Status { get; set; }

    }
}