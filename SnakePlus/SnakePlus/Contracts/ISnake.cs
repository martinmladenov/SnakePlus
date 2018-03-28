namespace SnakePlus.Contracts
{
    using System.Collections.Generic;
    using Models;
    using Models.Snakes;

    public interface ISnake
    {
        Direction CurrentDirection { get; set; }
        bool Dead { get; }
        int Extend { get; set; }
        Position Head { get; }
        HashSet<Position> Positions { get; }

        void Move();
    }
}