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
        public KeyboardState KeyboardState { get; private set; }

        public TileManager TileManager { get; private set; } = new TileManager();

        private readonly Camera Camera;
        private readonly Player Player;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Initialize player and camera
            Player = new Player(Drawing.Grid, Drawing.Grid);
            Camera = new Camera(Player);
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
            KeyboardState = Keyboard.GetState();
            ProcessKeyboardState(KeyboardState);

            Player.Update(gameTime, this); // Update player
            Camera.Update(gameTime, this); // Update camera

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, transformMatrix: Camera.Transform);
            TileManager.Draw(this); // Draw tiles
            Player.Draw(this); // Draw player
            SpriteBatch.End();

            SpriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            Player.DrawUI(this);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        private void ProcessKeyboardState(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Escape)) Exit();
        }
    }
}
