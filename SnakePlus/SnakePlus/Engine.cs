namespace SnakePlus
{
    using Contracts;
    using Models.Games;
    using Models.Snakes;

    public class Engine
    {
        public void InitGame()
        {
            IGame game = new SimpleGame(20, 20);
            game.Start(new SimpleSnake(game));
        }
    }
}
