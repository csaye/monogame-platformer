using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Platformer
{
    public class Player
    {
        private int X { get; set; }
        private int Y { get; set; }

        private Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set { X = (int)value.X; Y = (int)value.Y; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(X, Y, Drawing.Grid, Drawing.Grid); }
        }

        private Vector2 movementDirection = new Vector2(0, 1);
        private float movementSpeed = 100;

        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            ProcessKeyboardState(game.KeyboardState);

            Vector2 movementFactor = movementDirection * (float)gameTime.ElapsedGameTime.TotalSeconds * movementSpeed;
            //Position += movementFactor;
            Position = game.TileManager.TryMove(this, movementFactor);
        }

        public void Draw(Game1 game)
        {
            Drawing.DrawSprite(Drawing.PlayerTexture, Bounds, game, SortingLayers.Player);
        }

        private void ProcessKeyboardState(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.D)) movementDirection.X = 1;
            else if (state.IsKeyDown(Keys.A)) movementDirection.X = -1;
            else movementDirection.X = 0;
        }
    }
}
