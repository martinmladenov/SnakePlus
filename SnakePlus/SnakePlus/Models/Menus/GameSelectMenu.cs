namespace SnakePlus.Models.Menus
{
    using System;
    using Contracts;

    public class GameSelectMenu : IMenu
    {
        private int currentGameIndex;

        private IGame[] games;

        public GameSelectMenu(IGame[] games)
        {
            currentGameIndex = 0;
            this.games = games;
            Done = false;
        }

        public IGame SelectedGame => games[currentGameIndex];

        public string[] Text => new[] { "Select game", "", "", $"<      {games[currentGameIndex].GetType().Name}      >", "", "", "Press ENTER to confirm" };
        public int Width => 60;
        public int Height => 20;
        public bool Done { get; private set; }

        public void OnKeyPress(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                Done = true;
                return;
            }

            if (key == ConsoleKey.LeftArrow)
            {
                if (currentGameIndex == 0)
                {
                    currentGameIndex = games.Length - 1;
                }
                else
                {
                    currentGameIndex--;
                }
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (currentGameIndex == games.Length - 1)
                {
                    currentGameIndex = 0;
                }
                else
                {
                    currentGameIndex++;
                }
            }

            OutputWriter.DisplayMenu(this);
        }
    }
}
