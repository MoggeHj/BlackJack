using System.Collections.Generic;
using System.Linq;
using BlackJack.core.Interfaces;

namespace BlackJack.core
{
    public abstract class BaseBlackJack
    {
        protected IPlayer Dealer { get; set; }
        protected List<IPlayer> Players { get; set; }
        protected IDeck Deck { get; set; }
        protected int BustLimit { get; set; }

        public BaseBlackJack(IDeck deck, int bustLimit)
        {
            Deck = deck;
            BustLimit = bustLimit;
            Players = new List<IPlayer>();
            Dealer = new Player(){Name = "Dealer", Hand = new List<Card>()};
        }

        /// <summary>
        /// Decide if a players hand busts
        /// </summary>
        /// <param name="player"></param>
        /// <returns>
        /// True if the players hand busts
        /// False if the players hand is below bust limit
        /// </returns>
        public virtual bool Bust(IPlayer player)
        {
            if (CalculatePlayerHand(player) > BustLimit)
            {
                player.Status = Status.Lost;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Finds players optimal hand.
        /// </summary>
        /// <param name="player"> </param>
        /// <returns>
        /// Player's maximum hand below bust limit.
        /// </returns>
        public virtual int CalculatePlayerHand(IPlayer player)
        {
            var handWithoutAces = 0;
            foreach (var card in player.Hand.Where(card => card.Rank != 1))
            {
                if (card.Rank >= 10) handWithoutAces += 10;
                else handWithoutAces += card.Rank;
            }

            var countAces = player.Hand.Count(card => card.Rank == 1);

            switch (countAces)
            {
                case 0: return handWithoutAces;
                case 1: return handWithoutAces + 11 > BustLimit ? handWithoutAces + countAces : handWithoutAces + 11;
                default: return handWithoutAces + 11 + countAces - 1 > BustLimit ? handWithoutAces + countAces : handWithoutAces + 11 + countAces - 1;
            }
        }
        

        public virtual IPlayer GetWinner(IPlayer dealer, IPlayer player)
        {
            if (Bust(player)) return dealer;
            if (Bust(dealer)) return player;
            if (CalculatePlayerHand(dealer) == CalculatePlayerHand(player)) return dealer;
            if (CalculatePlayerHand(dealer) > CalculatePlayerHand(player)) return dealer;
            
            return player;
        }

        public virtual void RegisterPlayers(IEnumerable<IPlayer> players)
        {
            Players.AddRange(players.ToList());
        }

    }
}