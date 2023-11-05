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
        float collisionTimer;
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
        }

        public void Update(Player player, GameTime gameTime)
        {
            UpdateAnimations(gameTime);
            UpdateRectanglePos();
            PlayerCollision(player, gameTime);
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

        private void PlayerCollision(Player player, GameTime deltaTime)
        {
            if (rect.Intersects(player.rect) && !player.isHit)
            {
                Game1.RemoveLife();
                Game1.RemovePoints(10);
                player.isHit = true;
            }

            if (player.isHit)
            {
                collisionTimer -= (float)deltaTime.ElapsedGameTime.TotalSeconds;
            }
            if (collisionTimer <= 0)
            {
                player.isHit = false;
                collisionTimer = eventDelay;
            }
            Debug.WriteLine($"Timer: {collisionTimer} lives: {Game1.lives}");
        }

        private void UpdateAnimations(GameTime gameTime)
        {
            switch (currentDirection)
            {
                case EnemyDirection.Left:
                    leftAnimation.UpdateAnimation(gameTime);
                    break;

                case EnemyDirection.Right:
                    rightAnimation.UpdateAnimation(gameTime);
                    break;

                case EnemyDirection.Up:
                    upAnimation.UpdateAnimation(gameTime);
                    break;

                case EnemyDirection.Down:
                    downAnimation.UpdateAnimation(gameTime);
                    break;
            }
            leftAnimation.UpdateAnimationPosition(position);
            rightAnimation.UpdateAnimationPosition(position);
            upAnimation.UpdateAnimationPosition(position);
            downAnimation.UpdateAnimationPosition(position);
        }
    }
}
