using Microsoft.Xna.Framework;

namespace Platformer.Tiles
{
    public abstract class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Rectangle Bounds
        {
            get { return new Rectangle(X, Y, Drawing.Grid, Drawing.Grid); }
        }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public abstract void Draw(Game1 game);
    }
}
