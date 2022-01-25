using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.core.Interfaces;

namespace BlackJack.core
{
    public class Deck : IDeck

    {
        private static readonly Random Random = new Random();
        public Queue<Card> Cards { get; set; }

        public Deck()
        {
            var cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (int i = 1; i < 14; i++)
                {
                    cards.Add(new Card() {Rank = i, Suit = suit});
                }
            }

            Cards = new Queue<Card>(Shuffle(cards));
        }

        /// <summary>
        /// Gives a card from the deck
        /// </summary>
        /// <returns>One card</returns>
        public Card GiveCard()
        {
            return Cards.Dequeue();
        }

        /// <summary>
        /// Shuffle a deck of cards using Fisher-Yates shuffle.
        /// </summary>
        /// <returns>
        /// A shuffled collection of cards
        /// </returns>
        //TODO: Make method that can shuffle the collection we use.
        public IEnumerable<Card> Shuffle(IList<Card> cards)
        {
            int n = cards.ToList().Count;
            while (n > 1)
            {
                n--;
                int k = Random.Next(n + 1);
                (cards[k], cards[n]) = (cards[n], cards[k]);
            }

            return cards;
        }
    }
}
