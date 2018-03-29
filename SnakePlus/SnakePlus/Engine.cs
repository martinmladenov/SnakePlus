namespace SnakePlus
{
    using System;
    using Contracts;
    using Models.Games;
    using Models.Menus;
    using Models.Snakes;

    public class Engine
    {
        public void InitGame()
        {
            Console.CursorVisible = false;

            IMenu menu = new StartMenu();
            OutputWriter.DisplayMenu(menu);

            IGame game = SelectGame(20, 20);

            ISnake selectedSnake = SelectSnake(game);

            game.Start(selectedSnake);
        }

        private IGame SelectGame(int x, int y)
        {
            IGame[] availableGames =
            {
                new SimpleGame(x,y),
                new ReverseGame(x,y),
                new ChangingBarriersGame(x,y),
            };

            GameSelectMenu menu = new GameSelectMenu(availableGames);
            OutputWriter.DisplayMenu(menu);

            return menu.SelectedGame;
        }

        private ISnake SelectSnake(IGame game)
        {
            ISnake[] availableSnakes =
            {
                new SimpleSnake(game),
                new InfiniteSnake(game),
                new DegradingSnake(game),
            };

            SnakeSelectMenu menu = new SnakeSelectMenu(availableSnakes);
            OutputWriter.DisplayMenu(menu);

            return menu.SelectedSnake;
        }
    }
}
