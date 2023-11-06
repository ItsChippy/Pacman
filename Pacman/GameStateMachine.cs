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

        bool hasLost;
        StringBuilder gameStats = new StringBuilder();
        float timer;

        string gameTitle = "PACMAN";
        string startingText = "Press Enter to Play..";
        string easterEggText = "Press Z in game for a surprise..";

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
                timer = 300;
            }
        }

        public void PlayingUpdate(KeyboardState keys, GameTime gameTime, Game1 game1)
        {
            if (Game1.lives == 0 || timer == 0)
            {
                MediaPlayer.Stop();
                hasLost = true;
                game1.currentState = GameState.GameOver;
            }
            else if (game1.loadingManager.level.CheckIfWin())
            {
                MediaPlayer.Stop();
                hasLost = false;
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
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            PlayerController.Instance.Update(keys, gameTime);
            EnemyController.Instance.Update(gameTime);
            EnemyController.Instance.UpdatePlayerCollision(gameTime);
            game1.loadingManager.level.Update();
            gameStats.Clear();
            gameStats.Append($"Time: {timer:00}   Points: {Game1.points}");
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

        public void StartMenuDraw(SpriteBatch spriteBatch, Game1 game1)
        {
            Vector2 gameTitleMiddlePoint = game1.loadingManager.zombieFont.MeasureString(gameTitle) / 2;
            Vector2 startingTextMiddlePoint = game1.loadingManager.gameFont.MeasureString(startingText) / 2;
            game1.loadingManager.level.Draw(spriteBatch);
            spriteBatch.Draw(game1.blackRectangle, new Rectangle(0, 200, game1.Window.ClientBounds.Width, 200), Color.Black);
            spriteBatch.DrawString(game1.loadingManager.zombieFont, gameTitle, new Vector2(game1.Window.ClientBounds.Width / 2, 230), Color.Yellow, 0f, gameTitleMiddlePoint, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(game1.loadingManager.gameFont, startingText, new Vector2(game1.Window.ClientBounds.Width / 2, 320), Color.White, 0f, startingTextMiddlePoint, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(game1.loadingManager.zombieFont, easterEggText, new Vector2(0, game1.Window.ClientBounds.Height - 20), Color.Red, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 0f);
        }

        public void PlayingDraw(SpriteBatch spriteBatch, Game1 game1)
        {
            game1.loadingManager.level.Draw(spriteBatch);
            if (isZombieMode)
            {
                string zombieModeText = "It's Zombie Hours";
                Vector2 textMiddlePoint = game1.loadingManager.zombieFont.MeasureString(zombieModeText) / 2;
                EnemyController.Instance.DrawZombies(spriteBatch);
                spriteBatch.DrawString(game1.loadingManager.zombieFont, zombieModeText, new Vector2(game1.Window.ClientBounds.Width / 2, game1.Window.ClientBounds.Height / 2), Color.Red, 0f, textMiddlePoint, 1.5f, SpriteEffects.None, 0f);
            }
            PlayerController.Instance.Draw(spriteBatch);
            PlayerController.Instance.DrawLives(spriteBatch);
            EnemyController.Instance.Draw(spriteBatch);
            spriteBatch.DrawString(game1.loadingManager.gameFont, "Lives: ", Vector2.Zero, Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(game1.loadingManager.gameFont, gameStats, new Vector2(0, game1.Window.ClientBounds.Height - 30), Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
        }

        public void GameOverDraw(SpriteBatch spriteBatch, Game1 game1)
        {
            string gameOverText;
            Vector2 textMiddlePoint;
            if (hasLost)
            {
                gameOverText = "You Lost! Press Space to restart..." +
                                       $"\n     Points: {Game1.points}" +
                                       $"\n     Time left: {timer:00}";
                textMiddlePoint = game1.loadingManager.gameFont.MeasureString(gameOverText) / 2;
                spriteBatch.DrawString(game1.loadingManager.gameFont, gameOverText, new Vector2(game1.Window.ClientBounds.Width / 2, game1.Window.ClientBounds.Height / 2), Color.Red, 0f, textMiddlePoint, 1.5f, SpriteEffects.None, 0f);

            }
            else if (!hasLost)
            {
                gameOverText = "You Won! Press Space to play again..." +
                                       $"\n     Points: {Game1.points}" +
                                       $"\n     Time left: {timer:00}";
                textMiddlePoint = game1.loadingManager.gameFont.MeasureString(gameOverText) / 2;
                spriteBatch.DrawString(game1.loadingManager.gameFont, gameOverText, new Vector2(game1.Window.ClientBounds.Width / 2, game1.Window.ClientBounds.Height / 2), Color.Lime, 0f, textMiddlePoint, 1.5f, SpriteEffects.None, 0f);

            }
        }
    }
}
