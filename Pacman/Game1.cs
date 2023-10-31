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
            loadingManager = new LoadingManager(this);

            _graphics.PreferredBackBufferHeight = loadingManager.level.tileTexture.Height * Level.tileMap.GetLength(1);
            _graphics.PreferredBackBufferWidth = loadingManager.level.tileTexture.Width * Level.tileMap.GetLength(0);
            
            _graphics.ApplyChanges();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var keys = Keyboard.GetState();

            PlayerController.Instance.Update(keys, gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            loadingManager.level.Draw(_spriteBatch);
            PlayerController.Instance.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}