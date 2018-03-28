namespace SnakePlus.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface IGame
    {
        int AppleCounter { get; }
        Position AppleLocation { get; }
        int Height { get; }
        ISnake Snake { get; }
        int Width { get; }
        HashSet<Position> Obstructions { get; }

        void Start(ISnake snake);
    }
}