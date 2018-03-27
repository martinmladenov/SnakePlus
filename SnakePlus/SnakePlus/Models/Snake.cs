namespace SnakePlus.Models
{
    using System;
    using System.Collections.Generic;

    public class Snake
    {
        private Game game;
        private LinkedList<Position> positionsLinkedList;
        private Direction currentDirection;

        public Snake(Position startPos, int length, Game game)
        {
            positionsLinkedList = new LinkedList<Position>();
            positionsLinkedList.AddFirst(startPos);

            Extend = length;
            this.game = game;

            CurrentDirection = Direction.Up;
            Positions = new HashSet<Position> { startPos };

            Dead = false;
        }

        public HashSet<Position> Positions { get; }
        public Position Head => positionsLinkedList.First.Value;
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
            else
            {
                Positions.Remove(positionsLinkedList.Last.Value);

                positionsLinkedList.RemoveLast();
            }

            int currX = positionsLinkedList.First.Value.X;
            int currY = positionsLinkedList.First.Value.Y;

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

            if (Positions.Contains(nextPosition))
            {
                Dead = true;
            }

            positionsLinkedList.AddFirst(nextPosition);
            Positions.Add(nextPosition);
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}