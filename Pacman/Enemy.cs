using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Enemy : BaseGhost
    {

        float timer;
        const float eventDelay = 3f;

        Animation leftAnimation;
        Animation rightAnimation;
        Animation upAnimation;
        Animation downAnimation;

        public Enemy(Texture2D texture, Vector2 position) :base(texture, position) 
        {
            rightAnimation = new Animation(texture, 32, 32, 2, 200, 0, 0);
            leftAnimation = new Animation(texture, 32, 32, 2, 200, 64, 0);
            upAnimation = new Animation(texture, 32, 32, 2, 200, 128, 0);
            downAnimation = new Animation(texture, 32, 32, 2, 200, 192, 0);

            timer = eventDelay;
        }

        public void Update(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateAnimations(gameTime);
            base.Move(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentDirection) 
            {
                case EnemyDirection.Left:
                    leftAnimation.Draw(spriteBatch, 1f, SpriteEffects.None, Color.White, 0f, Vector2.Zero);
                    break;

                case EnemyDirection.Right:
                    rightAnimation.Draw(spriteBatch, 1f, SpriteEffects.None, Color.White, 0f, Vector2.Zero);
                    break;

                case EnemyDirection.Up:
                    upAnimation.Draw(spriteBatch, 1f, SpriteEffects.None, Color.White, 0f, Vector2.Zero);
                    break;

                case EnemyDirection.Down:
                    downAnimation.Draw(spriteBatch, 1f, SpriteEffects.None, Color.White, 0f, Vector2.Zero);
                    break;
            }
        }

        private void UpdateAnimations(GameTime gameTime)
        {
            switch (currentDirection)
            {
                case EnemyDirection.Left:
                    leftAnimation.UpdateAnimation(gameTime);
                    leftAnimation.UpdateAnimationPosition(position);
                    break;

                case EnemyDirection.Right:
                    rightAnimation.UpdateAnimation(gameTime);
                    rightAnimation.UpdateAnimationPosition(position);
                    break;

                case EnemyDirection.Up:
                    upAnimation.UpdateAnimation(gameTime);
                    upAnimation.UpdateAnimationPosition(position);
                    break;

                case EnemyDirection.Down:
                    downAnimation.UpdateAnimation(gameTime);
                    downAnimation.UpdateAnimationPosition(position);
                    break;
            }
        }
    }
}
