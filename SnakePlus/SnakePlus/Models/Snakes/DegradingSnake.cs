namespace SnakePlus.Models.Snakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public class DegradingSnake : ISnake
    {
        private IGame game;
        private Direction currentDirection;
        private Random rnd;

        public DegradingSnake(IGame game, int length = 3)
        {
            Position startPos = new Position(game.Width / 2, game.Height / 2);

            Head = startPos;

            Extend = length;
            this.game = game;

            CurrentDirection = Direction.Up;
            Positions = new HashSet<Position> { startPos };

            Dead = false;

            rnd = new Random();
        }

        public HashSet<Position> Positions { get; }
        public Position Head { get; private set; }
        public int Extend { get; set; }

        public Direction CurrentDirection
        {
            get => currentDirection;
            set
            {
                if (value == Direction.Up && CurrentDirection == Direction.Down ||
                    value == Direction.Down && CurrentDirection == Direction.Up ||
                    value == Direction.Left && CurrentDirection == Direction.Right ||
                    value == Direction.Right && CurrentDirection == Direction.Left)
                {
                    return;
                }

                currentDirection = value;
            }
        }

        public bool Dead { get; private set; }

        public void Move()
        {
            if (Extend > 0)
            {
                Extend--;
            }
            else if(rnd.Next(5) == 0)
            {
                Positions.Remove(Positions.Skip(rnd.Next(Positions.Count - 2)).First());
            }

            int currX = Head.X;
            int currY = Head.Y;

            int targetX;
            int targetY;

            switch (CurrentDirection)
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
                targetX = game.Width - 1;
            }
            else if (targetX == game.Width)
            {
                targetX = 0;
            }
            else if (targetY < 0)
            {
                targetY = game.Height - 1;
            }
            else if (targetY == game.Height)
            {
                targetY = 0;
            }

            Position nextPosition = new Position(targetX, targetY);

            if (Positions.Contains(nextPosition) || game.Obstructions.Contains(nextPosition))
            {
                Dead = true;
            }
            Positions.Add(nextPosition);
            Head = nextPosition;
        }
    }
}