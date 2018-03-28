namespace SnakePlus.Models.Menus
{
    using System;
    using Contracts;

    public class PauseMenu : IMenu
    {
        public PauseMenu()
        {
            Done = false;
        }

        public string[] Text => new[] { "Paused", "Press SPACE to resume" };

        public int Width => 27;

        public int Height => 6;

        public bool Done { get; private set; }

        public void OnKeyPress(ConsoleKey key)
        {
            if (key == ConsoleKey.Spacebar)
            {
                Done = true;
            }
        }
    }
}
