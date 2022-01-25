using System.Collections.Generic;

namespace BlackJack.core.Interfaces
{
    public interface IPlayer
    {
        string Name { get; set; }
        List<Card> Hand { get; set; }
        Status Status { get; set; }
    }
}