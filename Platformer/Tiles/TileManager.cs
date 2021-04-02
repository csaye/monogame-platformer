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
    }
}
