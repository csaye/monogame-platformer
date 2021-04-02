using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Platformer.Tiles
{
    public class TileManager
    {
        public List<Tile> Tiles { get; private set; } = new List<Tile>();

        public TileManager()
        {
            for (int x = 0; x < Drawing.Width; x += Drawing.Grid)
            {
                Add(new Stone(x, Drawing.Height - Drawing.Grid));
            }
        }

        public void Draw(Game1 game)
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw(game);
            }
        }

        public void Add(Tile tile) => Tiles.Add(tile);

        public Vector2 TryMove(Player player, Vector2 movementFactor)
        {
            Rectangle newBounds = player.Bounds;
            newBounds.X += (int)Math.Round(movementFactor.X);
            newBounds.Y += (int)Math.Round(movementFactor.Y);

            // Check each tile
            foreach (Tile tile in Tiles)
            {
                // If new bounds intersects, adjust bounds
                Rectangle tileBounds = tile.Bounds;
                if (newBounds.Intersects(tileBounds))
                {
                    Point newCenter = newBounds.Center;
                    Point objCenter = tileBounds.Center;

                    // If greater horizontal displacement
                    if (Math.Abs(newCenter.X - objCenter.X) > Math.Abs(newCenter.Y - objCenter.Y))
                    {
                        // If right of tile
                        if (newCenter.X > objCenter.X) newBounds.X = tileBounds.Right;
                        // If left of tile
                        else newBounds.X = tileBounds.Left - newBounds.Width;
                    }
                    // If greater vertical displacement
                    else
                    {
                        // If above tile
                        if (newCenter.Y > objCenter.Y) newBounds.Y = tileBounds.Bottom;
                        // If below tile
                        else newBounds.Y = tileBounds.Top - newBounds.Height;
                    }
                }
            }

            return new Vector2(newBounds.X, newBounds.Y);
        }
    }
}
