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
            IOManager.DisplayMenu(menu);

            IGame game = new SimpleGame(20, 20);

            ISnake selectedSnake = SelectSnake(game);

            game.Start(selectedSnake);
        }

        private ISnake SelectSnake(IGame game)
        {
            ISnake[] availableSnakes =
            {
                new SimpleSnake(game),
                new InfiniteSnake(game)
            };

            SnakeSelectMenu menu = new SnakeSelectMenu(availableSnakes);
            IOManager.DisplayMenu(menu);

            return menu.SelectedSnake;
        }
    }
}
