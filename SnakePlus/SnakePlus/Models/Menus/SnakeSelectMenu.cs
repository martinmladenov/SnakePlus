namespace SnakePlus.Models.Menus
{
    using System;
    using Contracts;

    public class SnakeSelectMenu : IMenu
    {
        private int currentSnakeIndex;

        private ISnake[] snakes;

        public SnakeSelectMenu(ISnake[] snakes)
        {
            currentSnakeIndex = 0;
            this.snakes = snakes;
            Done = false;
        }

        public ISnake SelectedSnake => snakes[currentSnakeIndex];

        public string[] Text => new[] { "Select snake", "", "", $"<      {snakes[currentSnakeIndex].GetType().Name}      >", "", "", "Press ENTER to confirm" };
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
                if (currentSnakeIndex == 0)
                {
                    currentSnakeIndex = snakes.Length - 1;
                }
                else
                {
                    currentSnakeIndex--;
                }
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (currentSnakeIndex == snakes.Length - 1)
                {
                    currentSnakeIndex = 0;
                }
                else
                {
                    currentSnakeIndex++;
                }
            }

            IOManager.DisplayMenu(this);
        }
    }
}
