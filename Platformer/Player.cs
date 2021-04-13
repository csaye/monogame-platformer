using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Platformer.Tiles;
using System;

namespace Platformer
{
    public class Player
    {
        private Tile[,] tiles;

        private Vector2 position;
        private Vector2 velocity;
        private bool grounded = false;

        private const float Epsilon = 0.1f;

        private const float MovementSpeed = 3;
        private const float JumpVelocity = -5;
        private const float Gravity = 6;

        private const float maxMagnitudeX = 10;
        private const float maxMagnitudeY = 100;

        public Rectangle Bounds
        {
            get
            {
                int x = (int)(position.X * Drawing.Grid);
                int y = (int)(position.Y * Drawing.Grid);
                return new Rectangle(x, y, Drawing.Grid, Drawing.Grid);
            }
        }

        public Player(float x, float y)
        {
            position = new Vector2(x, y);
            velocity = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds; // Get delta
            KeyboardState state = game.KeyboardState; // Get keyboard state
            //tiles = game.TileManager.tiles; // Get tiles
            tiles = null;

            // Walk left and right
            if (state.IsKeyDown(Keys.A)) velocity.X -= MovementSpeed * delta;
            if (state.IsKeyDown(Keys.D)) velocity.X += MovementSpeed * delta;
            // Jump if grounded
            if (grounded && state.IsKeyDown(Keys.Space)) velocity.Y = JumpVelocity;
            // Add gravity
            velocity.Y += Gravity * delta;

            // Clamp velocity
            velocity.X = Math.Clamp(velocity.X, -maxMagnitudeX, maxMagnitudeX);
            velocity.Y = Math.Clamp(velocity.Y, -maxMagnitudeY, maxMagnitudeY);

            // Calculate new and old positions
            Vector2 newPosition = position + velocity * delta;
            Console.WriteLine(newPosition);
            int posXA = newPosition.X < 0 ? -1 : (int)newPosition.X;
            int posXB = (int)(newPosition.X + 1 - Epsilon);
            int posYA = newPosition.Y < 0 ? -1 : (int)newPosition.Y;
            int posYB = (int)(newPosition.Y + 1 - Epsilon);
            grounded = false; // Reset grounded
            Console.WriteLine($"{posXA}, {posXB}, {posYA}, {posYB}");

            // Left collision
            if (velocity.X < 0)
            {
                if (TileAt(posXA, posYA) || TileAt(posXA, posYB))
                {
                    Console.WriteLine("left");
                    newPosition.X = posXA + 1;
                    velocity.X = 0;
                }
            }
            // Right collision
            else if (velocity.X > 0)
            {
                if (TileAt(posXA + 1, posYA) || TileAt(posXA + 1, posYB))
                {
                    Console.WriteLine("right");
                    newPosition.X = posXA;
                    velocity.X = 0;
                }
            }
            // Top collision
            if (velocity.Y < 0)
            {
                if (TileAt(posXA, posYA) || TileAt(posXB, posYA))
                {
                    Console.WriteLine("top");
                    newPosition.Y = posYA + 1;
                    velocity.Y = 0;
                }
            }
            // Bottom collision
            else if (velocity.Y > 0)
            {
                if (TileAt(posXA, posYA + 1) || TileAt(posXB, posYA + 1))
                {
                    Console.WriteLine("bottom");
                    newPosition.Y = posYA;
                    velocity.Y = 0;
                    grounded = true;
                }
            }

            // Set new position
            position = newPosition;
        }

        public void Draw(Game1 game)
        {
            Drawing.DrawSprite(Drawing.PlayerTexture, Bounds, game, SortingLayers.Player);
        }

        // Returns whether tile at given position
        private bool TileAt(int x, int y)
        {
            // If out of bounds, return true
            if (x < 0 || x > tiles.GetLength(0) - 1 || y < 0 || y > tiles.GetLength(1) - 1) return true;
            return tiles[x, y] != null;
        }
    }
}
