using BlackJack.core.Interfaces;

namespace BlackJack.core.Factories
{
    /// <summary>
    /// Implementation of Factory Method Pattern.
    /// </summary>
    public abstract class GameProviderFactory : IGameFactory
    {
        public abstract IGame CreateGame(string game);

        public IGame GetGame(string game)
        {
            return CreateGame(game);
        }
    }
}
