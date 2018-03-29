namespace SnakePlus.Models.Games
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Contracts;
    using Menus;
    using Snakes;

    public class SimpleGame : IGame
    {
        private Random random;

        public SimpleGame(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.Obstructions = new HashSet<Position>();

            this.random = new Random();

            this.AppleCounter = 0;
        }

        public int Width { get; }
        public int Height { get; }
        public ISnake Snake { get; private set; }
        public Position AppleLocation { get; private set; }
        public int AppleCounter { get; private set; }
        public HashSet<Position> Obstructions { get; }

        public void Start(ISnake snake)
        {
            this.Snake = snake;

            this.SpawnApple();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            Snake.CurrentDirection = Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            Snake.CurrentDirection = Direction.Right;
                            break;
                        case ConsoleKey.UpArrow:
                            Snake.CurrentDirection = Direction.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            Snake.CurrentDirection = Direction.Down;
                            break;
                        case ConsoleKey.Spacebar:
                            Pause();
                            break;
                    }
                }

                Snake.Move();

                if (Snake.Head.Equals(AppleLocation))
                {
                    SpawnApple();
                    AppleCounter++;
                    Snake.Extend++;
                }

                OutputWriter.Draw(this);

                if (Snake.Dead)
                {
                    OutputWriter.DisplayDeathMessage(this);
                    break;
                }

                Thread.Sleep(300);
            }
        }

        private void Pause()
        {
            PauseMenu pauseMenu = new PauseMenu();

            OutputWriter.DisplayMenu(pauseMenu);
        }

        private void SpawnApple()
        {
            AppleLocation = GenerateRandomPosition();
        }

        private Position GenerateRandomPosition()
        {
            Position pos;
            do
            {
                pos = new Position(random.Next(Width - 1), random.Next(Height - 1));
            } while (Snake.Positions.Contains(pos));

            return pos;
        }
    }
}
