namespace SnakePlus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OutputWriter.ResizeWindow();
            
            Engine engine = new Engine();
            engine.InitGame();
        }
    }
}
