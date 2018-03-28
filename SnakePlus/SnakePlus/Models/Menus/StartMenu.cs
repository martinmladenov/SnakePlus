namespace SnakePlus.Models.Menus
{
    using System;
    using Contracts;
    public class StartMenu : IMenu
    {
        public StartMenu()
        {
            Done = false;
        }

        public string[] Text => new[] { "Welcome to SnakePlus!", "", "Press ENTER to begin" };
        public int Width => 60;
        public int Height => 20;
        public bool Done { get; private set; }

        public void OnKeyPress(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                Done = true;
            }
        }
    }
}
