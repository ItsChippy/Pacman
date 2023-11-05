using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class GameStateMachine
    {

        protected static GameStateMachine instance;
        bool isZombieMode = false;
        public static GameStateMachine Instance
        {
            get
            {
                instance ??= new GameStateMachine();
                return instance;
            }
        }

        public void StartMenuUpdate(KeyboardState keys, Game1 game1)
        {
            if (keys.IsKeyDown(Keys.Enter))
            {
                game1.currentState = GameState.Playing;
            }
        }

        public void PlayingUpdate(KeyboardState keys, GameTime gameTime, Game1 game1)
        {
            if (Game1.lives == 0)
            {
                MediaPlayer.Stop();
                game1.currentState = GameState.GameOver;
            }
            if (keys.IsKeyDown(Keys.Z) && !isZombieMode)
            {
                isZombieMode = true;
                MediaPlayer.Play(game1.loadingManager.zombieModeSong);
            }
            if (isZombieMode)
            {
                EnemyController.Instance.UpdateZombies(gameTime);
            }
            PlayerController.Instance.Update(keys, gameTime);
            EnemyController.Instance.Update(gameTime);
            game1.loadingManager.level.Update();
        }

        public void GameOverUpdate(KeyboardState keys, Game1 game1)
        {
            isZombieMode = false;
            if(keys.IsKeyDown(Keys.Space))
            {
                game1.currentState = GameState.StartMenu;
                game1.Restart();
            }
        }

        public void StartMenuDraw()
        {

        }

        public void PlayingDraw(SpriteBatch spriteBatch, Game1 game1)
        {
            game1.loadingManager.level.Draw(spriteBatch);
            if (isZombieMode)
            {
                string zombieModeText = "It's Zombie Hours";
                Vector2 textMiddlePoint = game1.loadingManager.zombieFont.MeasureString(zombieModeText) / 2;
                EnemyController.Instance.DrawZombies(spriteBatch);
                spriteBatch.DrawString(game1.loadingManager.zombieFont, zombieModeText, new Vector2(game1.Window.ClientBounds.Width / 2, game1.Window.ClientBounds.Height / 2), Color.Red, 0f, textMiddlePoint, 3f, SpriteEffects.None, 0f);
            }
            PlayerController.Instance.Draw(spriteBatch);
            EnemyController.Instance.Draw(spriteBatch);
        }

        public void GameOverDraw(SpriteBatch spriteBatch, Game1 game1) 
        {
            string gameOverText = "You Lost!";
            Vector2 textMiddlePoint = game1.loadingManager.zombieFont.MeasureString(gameOverText) / 2;
            spriteBatch.DrawString(game1.loadingManager.zombieFont, gameOverText, new Vector2(game1.Window.ClientBounds.Width / 2, game1.Window.ClientBounds.Height / 2), Color.Red, 0f, textMiddlePoint, 3f, SpriteEffects.None, 0f);
        }
    }
}
