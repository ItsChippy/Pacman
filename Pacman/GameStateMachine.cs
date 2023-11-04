using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public static GameStateMachine Instance
        {
            get
            {
                instance ??= new GameStateMachine();
                return instance;
            }
        }

        public void StartMenuUpdate()
        {

        }

        public void PlayingUpdate(KeyboardState keys, GameTime gameTime, Game1 game1)
        {
            PlayerController.Instance.Update(keys, gameTime);
            EnemyController.Instance.Update(gameTime);
            game1.loadingManager.level.Update();
        }

        public void GameOverUpdate()
        {

        }

        public void StartMenuDraw()
        {

        }

        public void PlayingDraw(SpriteBatch spriteBatch, Game1 game1)
        {
            game1.loadingManager.level.Draw(spriteBatch);
            PlayerController.Instance.Draw(spriteBatch);
            EnemyController.Instance.Draw(spriteBatch);
        }

        public void GameOverDraw() 
        {

        }
    }
}
