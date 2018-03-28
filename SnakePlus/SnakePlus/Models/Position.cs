namespace SnakePlus.Models
{
    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override int GetHashCode()
        {
            return X >= Y ? X * X + X + Y : X + Y * Y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position))
            {
                return false;
            }

            Position pos = (Position)obj;

            return this.X == pos.X && this.Y == pos.Y;
        }
    }
}