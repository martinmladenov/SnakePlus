namespace SnakePlus
{
    using Models;

    public class Engine
    {
        public void InitGame()
        {
            Game game = new Game(20,20);
            game.Start();
        }
    }
}
