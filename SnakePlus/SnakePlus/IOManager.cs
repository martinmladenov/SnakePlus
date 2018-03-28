namespace SnakePlus
{
    using System;
    using System.Text;
    using Contracts;
    using Models;

    public static class IOManager
    {
        public static void Draw(IGame game)
        {
            string collectedApples = $"Apples collected: {game.AppleCounter}";

            Console.SetCursorPosition(
                (Console.WindowWidth - collectedApples.Length) / 2,
                (Console.WindowHeight - game.Height) / 2 - 2);

            Console.Write(collectedApples);

            for (int y = 0; y < game.Height; y++)
            {
                Console.SetCursorPosition(
                    Console.WindowWidth / 2 - game.Width,
                    (Console.WindowHeight - game.Height) / 2 + y);

                StringBuilder sb = new StringBuilder();
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
                    else if (game.Obstructions.Contains(currPosition))
                    {
                        toDraw = '#';
                    }
                    else if (game.AppleLocation.Equals(currPosition))
                    {
                        toDraw = 'o';
                    }
                    else
                    {
                        toDraw = '.';
                    }

                    sb.Append(toDraw + " ");
                }

                Console.Write(sb);
            }
        }

        public static void DisplayMenu(IMenu menu)
        {
            for (int y = 0; y < menu.Height; y++)
            {
                Console.SetCursorPosition(
                    (Console.WindowWidth - menu.Width) / 2,
                    (Console.WindowHeight - menu.Height) / 2 + y);

                char sideChar, midChar;
                if (y == 0)
                {
                    sideChar = ' ';
                    midChar = '-';
                }
                else if (y == menu.Height - 1)
                {
                    sideChar = ' ';
                    midChar = '-';
                }
                else
                {
                    sideChar = '|';
                    midChar = ' ';
                }
                Console.Write(sideChar + new string(midChar, menu.Width - 2) + sideChar);
            }

            for (int i = 0; i < menu.Text.Length; i++)
            {
                string msg = menu.Text[i];

                Console.SetCursorPosition(
                    (Console.WindowWidth - msg.Length) / 2,
                    (Console.WindowHeight - menu.Text.Length) / 2 + i);

                Console.Write(msg);
            }

            do
            {
                menu.OnKeyPress(Console.ReadKey(true).Key);
            } while (!menu.Done);
        }

        public static void DisplayDeathMessage(IGame game)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(
                Console.WindowWidth / 2 - 4,
                (Console.WindowHeight - game.Height) / 2 - 3);
            Console.Write("YOU DIED");

            Position snakePos = game.Snake.Head;

            Console.SetCursorPosition(
                Console.WindowWidth / 2 - game.Width + 2 * snakePos.X,
                (Console.WindowHeight - game.Height) / 2 + snakePos.Y);

            Console.Write('X');

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ResizeWindow()
        {
            Console.SetWindowSize(100, 30);
        }
    }
}
