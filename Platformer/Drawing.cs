using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    public static class Drawing
    {
        public static int Grid { get; } = 32;
        public static int Width { get; } = 16 * 32;
        public static int Height { get; } = 16 * 32;

        public static Texture2D StoneTexture { get; private set; }

        public static void Initialize(Game1 game)
        {
            game.Graphics.PreferredBackBufferWidth = Width;
            game.Graphics.PreferredBackBufferHeight = Height;
            game.Graphics.ApplyChanges();
        }

        public static void LoadContent(Game1 game)
        {
            StoneTexture = game.Content.Load<Texture2D>("Stone");
        }

        public static void DrawSprite(Texture2D texture, Rectangle rect, Game1 game, float depth)
        {
            game.SpriteBatch.Draw(texture, rect, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, depth);
        }
    }
}
