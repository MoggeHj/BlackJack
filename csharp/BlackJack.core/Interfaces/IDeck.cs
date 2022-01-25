using System.Collections.Generic;

namespace BlackJack.core.Interfaces
{
    public interface IDeck
    {
        Queue<Card> Cards { get; set; }
        Card GiveCard();
        IEnumerable<Card> Shuffle(IList<Card> cards);
    }
}