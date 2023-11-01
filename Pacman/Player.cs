using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Player : BaseGameObject
    {
        const float speed = 100f;

        public bool isMoving { get; private set; } = false;
        Vector2 direction;
        Vector2 destination;

        Animation animation;
        Color color;

        public Player(Texture2D texture, Vector2 position, ContentManager content) : base(texture, position)
        {
            Texture2D playerSheet = content.Load<Texture2D>(@"playersheet");
            animation = new Animation(playerSheet, 32, 32, 4);
            color = Color.White;
        }

        public void Move(GameTime gameTime)
        {
            if (!isMoving)
            {
                return;
            }
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateRectanglePos();
            animation.UpdateAnimation(gameTime);
            animation.UpdateAnimationPosition(position);
            if (Vector2.Distance(position, destination) < 1)
            {
                position = destination;
                isMoving = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteEffects effect, float rotation)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            animation.Draw(spriteBatch, 1f, effect, color, rotation, origin);
        }


        private Vector2 GetNewDestination(Vector2 direction)
        {
            int tileWidth = 32;
            return position + direction * tileWidth;
        }

        public void ChangeDirection(Vector2 inputDirection)
        {
            if (isMoving)
            {
                return;
            }
            direction = inputDirection;
            Vector2 newDestination = GetNewDestination(direction);
            TileType tileAtNewDestination = Level.CheckTileType(newDestination);

            if (tileAtNewDestination != TileType.Wall)
            {
                destination = newDestination;
                isMoving = true;
            }
        }
    }
}
