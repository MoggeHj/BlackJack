namespace BlackJack.core.Interfaces
{
    public interface IGame
    {
        string Name { get; }
        void Start();
        void Stop();
    }
}