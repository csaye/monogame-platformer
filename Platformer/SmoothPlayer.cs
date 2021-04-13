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

        private const float MaxMagnitudeX = 100;
        private const float MaxMagnitudeY = 100;

        private const float Epsilon = Drawing.Grid / 16;

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
            velocity.X = Math.Clamp(velocity.X, -MaxMagnitudeX, MaxMagnitudeX);
            velocity.Y = Math.Clamp(velocity.Y, -MaxMagnitudeY, MaxMagnitudeY);

            // Move player by velocity
            position += velocity;

            // Get corner positions
            Vector2 topLeft = position;
            Vector2 bottomLeft = position + new Vector2(0, Drawing.Grid);
            Vector2 topRight = position + new Vector2(Drawing.Grid, 0);
            Vector2 bottomRight = position + new Vector2(Drawing.Grid, Drawing.Grid);

            // Left collision
            if (velocity.X < 0)
            {
                if (game.TileManager.WallAt(topLeft + new Vector2(0, Epsilon))
                    || game.TileManager.WallAt(bottomLeft - new Vector2(0, Epsilon)))
                {
                    velocity.X = 0;
                    position.X = (int)Math.Ceiling(position.X / Drawing.Grid) * Drawing.Grid;
                }
            }
            // Right collision
            else if (velocity.X > 0)
            {
                if (game.TileManager.WallAt(topRight + new Vector2(0, Epsilon))
                    || game.TileManager.WallAt(bottomRight - new Vector2(0, Epsilon)))
                {
                    velocity.X = 0;
                    position.X = (int)Math.Floor(position.X / Drawing.Grid) * Drawing.Grid;
                }
            }
            // Top collision
            if (velocity.Y < 0)
            {
                if (game.TileManager.WallAt(topLeft + new Vector2(Epsilon, 0))
                    || game.TileManager.WallAt(topRight - new Vector2(Epsilon, 0)))
                {
                    velocity.Y = 0;
                    position.Y = (int)Math.Ceiling(position.Y / Drawing.Grid) * Drawing.Grid;
                }
            }
            // Bottom collision
            else if (velocity.Y > 0)
            {
                if (game.TileManager.WallAt(bottomLeft + new Vector2(Epsilon, 0))
                    || game.TileManager.WallAt(bottomRight - new Vector2(Epsilon, 0)))
                {
                    velocity.Y = 0;
                    position.Y = (int)Math.Floor(position.Y / Drawing.Grid) * Drawing.Grid;
                }
            }
        }

        public void Draw(Game1 game)
        {
            Drawing.DrawSprite(Drawing.PlayerTexture, Bounds, game, SortingLayers.Player);

            Drawing.DrawText($"pos: {position}", new Vector2(8, 8), Color.White, game, SortingLayers.Text);
        }
    }
}
