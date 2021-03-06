using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    public static class Drawing
    {
        public const int Grid = 32;
        public const int GridWidth = 32;
        public const int GridHeight = 16;
        public const int Width = Grid * GridWidth;
        public const int Height = Grid * GridHeight;

        public static Texture2D StoneTexture { get; private set; }
        public static Texture2D PlayerTexture { get; private set; }

        private static SpriteFont arialFont;

        public static void Initialize(Game1 game)
        {
            game.Graphics.PreferredBackBufferWidth = Width;
            game.Graphics.PreferredBackBufferHeight = Height;
            game.Graphics.ApplyChanges();
        }

        public static void LoadContent(Game1 game)
        {
            StoneTexture = game.Content.Load<Texture2D>("Stone");
            PlayerTexture = game.Content.Load<Texture2D>("Player");

            arialFont = game.Content.Load<SpriteFont>("Arial");
        }

        public static void DrawSprite(Texture2D texture, Rectangle rect, Game1 game, float depth)
        {
            game.SpriteBatch.Draw(texture, rect, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, depth);
        }

        public static void DrawText(string text, Vector2 position, Color color, Game1 game, float depth)
        {
            game.SpriteBatch.DrawString(arialFont, text, position, color, 0, new Vector2(0, 0), 1, SpriteEffects.None, depth);
        }
    }
}
