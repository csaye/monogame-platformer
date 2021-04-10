namespace Platformer.Tiles
{
    public class TileManager
    {
        public Tile[,] tiles;

        public string tilemap;

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

            int width = Drawing.GridWidth;
            int height = Drawing.GridHeight;
            tiles = new Tile[width, height];
            for (int x = 0; x < Drawing.GridWidth; x++)
            {
                for (int y = 0; y < Drawing.GridHeight; y++)
                {
                    char tile = tilemap[y * Drawing.GridWidth + x];
                    if (tile == '#') tiles[x, y] = new Stone(x * Drawing.Grid, y * Drawing.Grid);
                }
            }
        }

        public void Draw(Game1 game)
        {
            foreach (Tile tile in tiles)
            {
                if (tile != null)
                {
                    tile.Draw(game);
                }
            }
        }
    }
}
