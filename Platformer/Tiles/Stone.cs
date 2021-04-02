namespace Platformer.Tiles
{
    public class Stone : Tile
    {
        public Stone(int x, int y) : base(x, y) {}

        public override void Draw(Game1 game)
        {
            Drawing.DrawSprite(Drawing.StoneTexture, Bounds, game, SortingLayers.Tiles);
        }
    }
}
