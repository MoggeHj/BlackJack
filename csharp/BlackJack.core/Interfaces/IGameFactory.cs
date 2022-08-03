namespace BlackJack.core.Interfaces
{
    public interface IGameFactory
    {
        IGame GetGame(string game);
    }
}