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

            Console.Clear();

            IGame game = new SimpleGame(20, 20);
            game.Start(new SimpleSnake(game));
        }
    }
}
