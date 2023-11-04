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
    public enum EnemyDirection
    {
        Left,
        Right,
        Down,
        Up,
    }
    public class BaseGhost
    {
        protected const float speed = 100f;

        protected Texture2D texture;
        protected Vector2 position;
        public Rectangle rect;

        protected EnemyDirection currentDirection;
        Random randomDirection;
        protected Vector2 direction;

        public BaseGhost(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            randomDirection = new Random();
            direction = new Vector2(GenerateRandomDirection(), 0); //randomizes starting direction to be left or right
            
            rect = new Rectangle((int)position.X, (int)position.Y, 32, 32);
        }

        public virtual void Move(GameTime gameTime)
        {
            if (Level.CheckTileType(GetNextTile(direction)) != TileType.Wall)
            {
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        protected void UpdateRectanglePos()
        {
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
        }

        private float GenerateRandomDirection()
        {
            return randomDirection.Next(2) * 2 - 1;
        }

        private Vector2 GetNextTile(Vector2 direction)
        {
            return position + direction * 32;
        }
    }
}
