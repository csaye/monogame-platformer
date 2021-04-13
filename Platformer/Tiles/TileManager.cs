using Microsoft.Xna.Framework;
using System;

namespace Platformer.Tiles
{
    public class TileManager
    {
        //public Tile[,] tiles;

        private string tilemap;

        public TileManager()
        {
            // Initialize tilemap
            tilemap += "................................";
            tilemap += "................................";
            tilemap += "................................";
            tilemap += "................................";
            tilemap += "................................";
            tilemap += "................................";
            tilemap += "................................";
            tilemap += "................................";
            tilemap += "....................#.#.#.......";
            tilemap += "....................###.#.......";
            tilemap += "....................#.#.#.......";
            tilemap += "................................";
            tilemap += "................................";
            tilemap += "####............................";
            tilemap += "################################";
            tilemap += "################################";

            //int width = Drawing.GridWidth;
            //int height = Drawing.GridHeight;
            //tiles = new Tile[width, height];
            //for (int x = 0; x < Drawing.GridWidth; x++)
            //{
            //    for (int y = 0; y < Drawing.GridHeight; y++)
            //    {
            //        char tile = tilemap[y * Drawing.GridWidth + x];
            //        if (tile == '#') tiles[x, y] = new Stone(x * Drawing.Grid, y * Drawing.Grid);
            //    }
            //}
        }

        public void Draw(Game1 game)
        {
            //foreach (Tile tile in tiles)
            //{
            //    if (tile != null)
            //    {
            //        tile.Draw(game);
            //    }
            //}
            for (int x = 0; x < Drawing.GridWidth; x++)
            {
                for (int y = 0; y < Drawing.GridHeight; y++)
                {
                    // If wall, draw wall
                    char tile = tilemap[y * Drawing.GridWidth + x];
                    if (tile == '#')
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
            if (x < 0 || x >= Drawing.GridWidth || y < 0 || y >= Drawing.GridHeight) return true;

            // Return tile at index
            int index = (int)Math.Floor(y) * Drawing.GridWidth + (int)Math.Floor(x);
            return tilemap[index] == '#';
        }
    }
}
