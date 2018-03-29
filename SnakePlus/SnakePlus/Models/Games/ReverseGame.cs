namespace SnakePlus.Models.Games
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Contracts;
    using Menus;
    using Snakes;

    public class ReverseGame : IGame
    {
        private Random random;
        private Direction appleDirection;

        public ReverseGame(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.Obstructions = new HashSet<Position>();

            this.random = new Random();

            this.AppleCounter = 0;
            this.appleDirection = Direction.Up;
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
                            appleDirection = Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            appleDirection = Direction.Right;
                            break;
                        case ConsoleKey.UpArrow:
                            appleDirection = Direction.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            appleDirection = Direction.Down;
                            break;
                        case ConsoleKey.Spacebar:
                            Pause();
                            break;
                    }
                }

                if (random.Next(5) == 0)
                {
                    int nextDirectionId = random.Next(2);

                    if (nextDirectionId == 0)
                    {
                        if (Snake.CurrentDirection == Direction.Up)
                            Snake.CurrentDirection = Direction.Right;
                        else if (Snake.CurrentDirection == Direction.Right)
                            Snake.CurrentDirection = Direction.Down;
                        else if (Snake.CurrentDirection == Direction.Down)
                            Snake.CurrentDirection = Direction.Left;
                        else if (Snake.CurrentDirection == Direction.Left)
                            Snake.CurrentDirection = Direction.Up;
                    }
                    else
                    {
                        if (Snake.CurrentDirection == Direction.Up)
                            Snake.CurrentDirection = Direction.Left;
                        else if (Snake.CurrentDirection == Direction.Right)
                            Snake.CurrentDirection = Direction.Up;
                        else if (Snake.CurrentDirection == Direction.Down)
                            Snake.CurrentDirection = Direction.Right;
                        else if (Snake.CurrentDirection == Direction.Left)
                            Snake.CurrentDirection = Direction.Down;
                    }
                }

                int currX = AppleLocation.X;
                int currY = AppleLocation.Y;

                int targetX;
                int targetY;

                switch (appleDirection)
                {
                    case Direction.Up:
                        targetX = currX;
                        targetY = currY - 1;
                        break;
                    case Direction.Down:
                        targetX = currX;
                        targetY = currY + 1;
                        break;
                    case Direction.Left:
                        targetX = currX - 1;
                        targetY = currY;
                        break;
                    case Direction.Right:
                        targetX = currX + 1;
                        targetY = currY;
                        break;
                    default:
                        throw new InvalidOperationException("Invalid direction");
                }

                if (targetX < 0)
                {
                    targetX = Width - 1;
                }
                else if (targetX == Width)
                {
                    targetX = 0;
                }
                else if (targetY < 0)
                {
                    targetY = Height - 1;
                }
                else if (targetY == Height)
                {
                    targetY = 0;
                }

                AppleLocation = new Position(targetX, targetY);

                if (Snake.Head.Equals(AppleLocation))
                {
                    SpawnApple();
                    AppleCounter++;
                    Snake.Extend++;
                }

                OutputWriter.Draw(this);

                if (!Snake.Head.Equals(AppleLocation) && Snake.Positions.Contains(AppleLocation))
                {
                    OutputWriter.DisplayDeathMessage(this);
                    break;
                }

                if (random.Next(2) == 0)
                {
                    Snake.Move();
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
