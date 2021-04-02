using Microsoft.Xna.Framework;

namespace Platformer
{
    public class Player
    {
        private int X { get; set; }
        private int Y { get; set; }

        private Rectangle Bounds
        {
            get { return new Rectangle(X, Y, Drawing.Grid, Drawing.Grid); }
        }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Update(GameTime gameTime, Game1 game)
        {

        }

        public void Draw(Game1 game)
        {
            Drawing.DrawSprite(Drawing.PlayerTexture, Bounds, game, SortingLayers.Player);
        }
    }
}
