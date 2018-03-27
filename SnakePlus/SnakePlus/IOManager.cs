namespace SnakePlus
{
    using System;
    using System.Text;
    using Models;

    public static class IOManager
    {
        public static void Draw(Game game)
        {
            Console.SetCursorPosition(0, 0);

            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < game.Height; y++)
            {
                for (int x = 0; x < game.Width; x++)
                {
                    Position currPosition = new Position(x, y);
                    char toDraw;

                    if (game.Snake.Head.Equals(currPosition))
                    {
                        toDraw = 'X';
                    }
                    else if (game.Snake.Positions.Contains(currPosition))
                    {
                        toDraw = 'x';
                    }
                    else if (game.AppleLocation.Equals(currPosition))
                    {
                        toDraw = 'ó';
                    }
                    else
                    {
                        toDraw = '.';
                    }

                    sb.Append(toDraw + " ");
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }

        public static void DisplayDeathMessage(Game game)
        {
            Console.Clear();
            Console.WriteLine("YOU DIED");
            Console.WriteLine($"Apples collected: {game.AppleCounter}");
        }
    }
}
