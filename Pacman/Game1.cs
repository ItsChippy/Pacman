using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Windows.Forms.Automation;

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
        internal GameState currentState;

        //player score
        public static int lives { get; private set; }
        static int points;

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

            currentState = GameState.Playing;
            lives = 3;
            points = 0;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var keys = Keyboard.GetState();

            switch(currentState)
            {
                case GameState.StartMenu:

                    break;

                case GameState.Playing:
                    GameStateMachine.Instance.PlayingUpdate(keys, gameTime, this);
                    break;

                case GameState.EatGhosts:

                    break;

                case GameState.GameOver:

                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (currentState)
            {
                case GameState.StartMenu:

                    break;

                case GameState.Playing:
                    GameStateMachine.Instance.PlayingDraw(_spriteBatch, this);
                    break;

                case GameState.EatGhosts:

                    break;

                case GameState.GameOver:

                    break;
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void AddPoints(int pointsToAdd)
        {
            points += pointsToAdd;
        }

        public static void RemovePoints(int pointsToRemove) 
        {
            points -= pointsToRemove;
        }

        public static void RemoveLife()
        {
            lives -= 1;  
        }

    }
}