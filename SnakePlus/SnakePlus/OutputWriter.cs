namespace SnakePlus
{
    using System;
    using System.Text;
    using Contracts;
    using Models;

    public static class OutputWriter
    {
        public static void Draw(IGame game)
        {
            string collectedApples = $"Apples collected: {game.AppleCounter}";

            Console.SetCursorPosition(
                (Console.WindowWidth - collectedApples.Length) / 2,
                (Console.WindowHeight - game.Height) / 2 - 2);

            Console.Write(collectedApples);

            Console.ForegroundColor = ConsoleColor.DarkGray;

            for (int y = 0; y < game.Height; y++)
            {
                Console.SetCursorPosition(
                    Console.WindowWidth / 2 - game.Width,
                    (Console.WindowHeight - game.Height) / 2 + y);

                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < game.Width; x++)
                {
                    sb.Append("· ");
                }

                Console.Write(sb);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(
                Console.WindowWidth / 2 - game.Width + 2 * game.AppleLocation.X,
                (Console.WindowHeight - game.Height) / 2 + game.AppleLocation.Y);
            Console.Write('o');

            Console.ForegroundColor = ConsoleColor.DarkRed;
            foreach (var obstruction in game.Obstructions)
            {
                Console.SetCursorPosition(
                    Console.WindowWidth / 2 - game.Width + 2 * obstruction.X,
                    (Console.WindowHeight - game.Height) / 2 + obstruction.Y);
                Console.Write('#');
            }

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var position in game.Snake.Positions)
            {
                Console.SetCursorPosition(
                    Console.WindowWidth / 2 - game.Width + 2 * position.X,
                    (Console.WindowHeight - game.Height) / 2 + position.Y);
                Console.Write('x');
            }

            Console.SetCursorPosition(
                Console.WindowWidth / 2 - game.Width + 2 * game.Snake.Head.X,
                (Console.WindowHeight - game.Height) / 2 + game.Snake.Head.Y);
            Console.Write('X');

            Console.ForegroundColor = ConsoleColor.Gray;
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

            Console.Clear();
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

            Console.ForegroundColor = ConsoleColor.DarkRed;

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
