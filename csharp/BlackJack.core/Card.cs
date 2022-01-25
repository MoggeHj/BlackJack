namespace BlackJack.core
{
    public class Card
    {
        public Suit Suit;
        public int Rank;

        public string GetRank()
        {
            switch (Rank)
            {
                case 1: return "A";
                case 11: return "J";
                case 12: return "Q";
                case 13: return "K";
                default: return Rank.ToString();
            }

            
        }

    }
}
