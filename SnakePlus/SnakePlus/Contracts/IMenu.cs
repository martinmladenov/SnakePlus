namespace SnakePlus.Contracts
{
    using System;

    public interface IMenu
    {
        string[] Text { get; }
        int Width { get; }
        int Height { get; }
        bool Done { get; }

        void OnKeyPress(ConsoleKey key);
    }
}
