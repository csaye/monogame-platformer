using Microsoft.Xna.Framework;
using System;
using System.Text;

namespace Platformer.Tiles
{
    public class TileManager
    {
        private readonly string Tilemap;

        public const int TilesWidth = 128;
        public const int TilesHeight = 16;
        public const int Width = TilesWidth * Drawing.Grid;
        public const int Height = TilesHeight * Drawing.Grid;

        public TileManager()
        {
            // Initialize tilemap
            for (int y = 0; y < TilesHeight; y++)
            {
                Tilemap += GetTilemapString(y);
            }
        }

        private string GetTilemapString(int y)
        {
            Random random = new Random();

            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < TilesWidth; x++)
            {
                if (y > TilesHeight - 2 && x < 8) sb.Append('#');
                else
                {
                    if (random.NextDouble() < 0.1) sb.Append('#');
                    else sb.Append('.');
                }
            }
            return sb.ToString();
        }

        public void Draw(Game1 game)
        {
            for (int x = 0; x < TilesWidth; x++)
            {
                for (int y = 0; y < TilesHeight; y++)
                {
                    // If wall, draw wall
                    if (WallAt(x, y))
                    {
                        Rectangle rect = new Rectangle(x * Drawing.Grid, y * Drawing.Grid, Drawing.Grid, Drawing.Grid);
                        Drawing.DrawSprite(Drawing.StoneTexture, rect, game, SortingLayers.Tiles);
                    }
                }
            }
        }

        // Returns whether wall at given position
        public bool WallAt(Vector2 pos) => WallAt(pos.X / Drawing.Grid, pos.Y / Drawing.Grid);
        public bool WallAt(float x, float y)
        {
            // If out of range, return wall
            if (x < 0 || x >= TilesWidth || y < 0 || y >= TilesHeight) return true;

            // Return tile at index
            int index = (int)Math.Floor(y) * TilesWidth + (int)Math.Floor(x);
            return Tilemap[index] == '#';
        }
    }
}
