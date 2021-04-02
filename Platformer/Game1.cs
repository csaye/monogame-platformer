using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Tiles;

namespace Platformer
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public TileManager TileManager { get; private set; } = new TileManager();

        private Player player = new Player(0, 0);

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Drawing.Initialize(this);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Drawing.LoadContent(this);
        }

        protected override void Update(GameTime gameTime)
        {
            ProcessKeyboardState(Keyboard.GetState());

            // Update player
            player.Update(gameTime, this);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);

            // Draw tiles
            TileManager.Draw(this);

            // Draw player
            player.Draw(this);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        private void ProcessKeyboardState(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Escape)) Exit();
        }
    }
}
