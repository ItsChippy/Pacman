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
        Random randomDirection; //randomizes direction (chooses between -1 and 1)
        Random randomChosenDirection; //for the pathfinding to choose a random direction once a wall is hit
        protected bool isMoving = false;
        protected Vector2 direction;
        protected Vector2 destination;

        public BaseGhost(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            randomDirection = new Random();
            randomChosenDirection = new Random();
            direction = new Vector2(GenerateRandomDirection(), 0); //randomizes starting direction to be left or right
            
            rect = new Rectangle((int)position.X, (int)position.Y, 32, 32);
        }

        public virtual void Move(GameTime gameTime)
        {
            if (!isMoving)
            {
                destination = GetNextTile(direction);
                isMoving = true;
            }
            if (Level.CheckTileType(destination) == TileType.Wall)
            {
                isMoving = false;
                if (randomChosenDirection.Next(2) == 0)
                {
                    direction = new Vector2(GenerateRandomDirection(), 0);
                }
                else
                {
                    direction = new Vector2(0, GenerateRandomDirection());
                }
                UpdateEnemyDirectionEnum(direction);
                return;
            }
            if (Vector2.Distance(position, destination) > 1)
            {
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }
            position = destination;
            isMoving = false;
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

        private void UpdateEnemyDirectionEnum(Vector2 direction)
        {
            if (direction.X == -1)
            {
                currentDirection = EnemyDirection.Left;
            }
            else if (direction.X == 1)
            {
                currentDirection = EnemyDirection.Right;
            }
            else if (direction.Y == -1)
            {
                currentDirection = EnemyDirection.Up;
            }
            else if (direction.Y == 1)
            {
                currentDirection = EnemyDirection.Down;
            }
        }
    }
}
