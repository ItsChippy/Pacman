using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public enum GameState
    {
        StartMenu,
        Playing,
        EatGhosts,
        GameOver
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public LoadingManager loadingManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            loadingManager = new LoadingManager(Content);

            _graphics.PreferredBackBufferHeight = loadingManager.tileTexture.Height * loadingManager.tileMap.GetLength(1);
            _graphics.PreferredBackBufferWidth = loadingManager.tileTexture.Width * loadingManager.tileMap.GetLength(0);
            _graphics.ApplyChanges();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            for (int rows = 0; rows < loadingManager.tileMap.GetLength(0); rows++)
            {
                for (int cols = 0; cols < loadingManager.tileMap.GetLength(1); cols++)
                {
                    loadingManager.tileMap[rows, cols].Draw(_spriteBatch);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}