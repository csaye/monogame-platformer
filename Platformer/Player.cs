using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Platformer
{
    public class Player
    {
        private Vector2 position;
        private Vector2 velocity;

        private const float Speed = 2;

        private const float MaxMagnitudeX = 4;
        private const float MaxMagnitudeY = 8;

        private const float Gravity = -7;
        private const float JumpVelocity = 5;

        private const float FrictionFactor = 0.95f;

        private const float Epsilon = Drawing.Grid / 4;
        private const float FrictionEpsilon = 0.1f;

        private bool grounded;

        public Vector2 GetPosition() => position;

        private Rectangle Bounds
        {
            get { return new Rectangle(position.ToPoint(), new Point(Drawing.Grid)); }
        }

        public Player(int x, int y)
        {
            position = new Vector2(x, y);
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            // Get delta and keyboard state
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState state = game.KeyboardState;

            // Get velocity
            velocity.Y -= delta * Gravity;
            bool dDown = state.IsKeyDown(Keys.D);
            bool aDown = state.IsKeyDown(Keys.A);
            if (dDown) velocity.X += delta * Speed;
            if (aDown) velocity.X -= delta * Speed;

            // If grounded
            if (grounded)
            {
                // Jump
                if (state.IsKeyDown(Keys.Space)) velocity.Y = -JumpVelocity;
                // Slow down by friction
                if (!aDown && !dDown)
                {
                    velocity.X *= FrictionFactor;
                    if (Math.Abs(velocity.X) < FrictionEpsilon) velocity.X = 0;
                }
            }

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

            grounded = false;
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
                    grounded = true;
                }
            }
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
        }

        public void Draw(Game1 game)
        {
            Drawing.DrawSprite(Drawing.PlayerTexture, Bounds, game, SortingLayers.Player);
        }

        public void DrawUI(Game1 game)
        {
            Drawing.DrawText($"pos: {position}", new Vector2(8, 8), Color.White, game, SortingLayers.Text);
            Drawing.DrawText($"vel: {velocity}", new Vector2(8, 24), Color.White, game, SortingLayers.Text);
        }
    }
}
