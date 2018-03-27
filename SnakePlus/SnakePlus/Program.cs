namespace SnakePlus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IOManager.ResizeWindow();
            
            Engine engine = new Engine();
            engine.InitGame();
        }
    }
}
