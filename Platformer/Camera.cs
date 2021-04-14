using Microsoft.Xna.Framework;
using Platformer.Tiles;
using System;

namespace Platformer
{
    public class Camera
    {
        private readonly Player Player;
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; private set; }

        public Camera(Player player)
        {
            Player = player;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            int midWidth = Drawing.Width / 2;
            int midHeight = Drawing.Height / 2;
            int sceneWidth = TileManager.Width;
            int sceneHeight = TileManager.Height;

            Vector2 playerPosition = Player.GetPosition();
            int cameraX = Math.Clamp((int)playerPosition.X, midWidth, sceneWidth - midWidth);
            int cameraY = Math.Clamp((int)playerPosition.Y, midHeight, sceneHeight - midHeight);

            Position = new Vector2(cameraX - midWidth, cameraY - midHeight);

            cameraX *= -1;
            cameraY *= -1;

            Matrix position = Matrix.CreateTranslation(cameraX, cameraY, 0);
            Matrix offset = Matrix.CreateTranslation(midWidth, midHeight, 0);

            Transform = position * offset;
        }
    }
}
