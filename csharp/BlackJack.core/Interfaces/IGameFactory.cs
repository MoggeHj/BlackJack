namespace BlackJack.core.Interfaces
{
    public interface IGameFactory
    {
        IGame Create(string game);
    }
}