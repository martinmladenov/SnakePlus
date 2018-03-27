namespace SnakePlus.Models
{
    using System;
    using System.Threading;

    public class Game
    {
        private Random random;

        public Game(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.random = new Random();

            this.Snake = new Snake(new Position(Width/2,Height/2), 3, this);
            this.AppleCounter = 0;

            this.SpawnApple();
        }

        public int Width { get; }
        public int Height { get; }
        public Snake Snake { get; }
        public Position AppleLocation { get; private set; }
        public int AppleCounter { get; private set; }

        public void Start()
        {
            while (true)
            {
                IOManager.Draw(this);

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            Snake.CurrentDirection = Snake.Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            Snake.CurrentDirection = Snake.Direction.Right;
                            break;
                        case ConsoleKey.UpArrow:
                            Snake.CurrentDirection = Snake.Direction.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            Snake.CurrentDirection = Snake.Direction.Down;
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

                if (Snake.Dead)
                {
                    IOManager.Draw(this);
                    IOManager.DisplayDeathMessage(this);
                    break;
                }

                Thread.Sleep(300);
            }
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
