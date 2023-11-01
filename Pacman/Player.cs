using Microsoft.Xna.Framework;
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

        Color color;

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
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

            if (Vector2.Distance(position, destination) < 1)
            {
                position = destination;
                isMoving = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteEffects effect, float rotation)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            spriteBatch.Draw(texture, position + origin, null, color, rotation, origin, 1f, effect, 0f);
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
