using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Platformer
{
    public class SmoothPlayer
    {
        private Vector2 position;
        private Vector2 velocity;

        private const float Speed = 1;

        private const float maxMagnitudeX = 100;
        private const float maxMagnitudeY = 100;

        private Rectangle Bounds
        {
            get { return new Rectangle(position.ToPoint(), new Point(Drawing.Grid)); }
        }

        public SmoothPlayer(int x, int y)
        {
            position = new Vector2(x, y);
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            // Get delta and keyboard state
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState state = game.KeyboardState;

            // Get velocity
            if (state.IsKeyDown(Keys.D)) velocity.X += delta * Speed;
            if (state.IsKeyDown(Keys.A)) velocity.X -= delta * Speed;
            if (state.IsKeyDown(Keys.S)) velocity.Y += delta * Speed;
            if (state.IsKeyDown(Keys.W)) velocity.Y -= delta * Speed;

            // Clamp velocity
            velocity.X = Math.Clamp(velocity.X, -maxMagnitudeX, maxMagnitudeX);
            velocity.Y = Math.Clamp(velocity.Y, -maxMagnitudeY, maxMagnitudeY);

            // Move player by velocity
            position += velocity;
        }

        public void Draw(Game1 game)
        {
            Drawing.DrawSprite(Drawing.PlayerTexture, Bounds, game, SortingLayers.Player);
        }
    }
}
