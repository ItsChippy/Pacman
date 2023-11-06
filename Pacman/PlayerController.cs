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
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public sealed class PlayerController
    {
        Direction currentDirection;
        float rotation;
        List<Vector2> livesPosition;
        public static PlayerController Instance { get; private set; }
        public readonly Player player;

        public PlayerController(Player player)
        {
            this.player = player;
            Instance = this;
            livesPosition = new List<Vector2>()
            {
                new Vector2(70, 0),
                new Vector2(100, 0),
                new Vector2(130, 0)
            };
        }
        public void Update(KeyboardState keys, GameTime gameTime)
        {
            UpdateDirectionInput(keys);
            switch (currentDirection) //input handling for movement
            {
                case Direction.Left:
                    player.ChangeDirection(new Vector2(-1, 0));
                    break;

                case Direction.Right:
                    player.ChangeDirection(new Vector2(1, 0));
                    break;

                case Direction.Up:
                    player.ChangeDirection(new Vector2(0, -1));
                    break;

                case Direction.Down:
                    player.ChangeDirection(new Vector2(0, 1));
                    break;
            }
            player.Move(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentDirection)
            {
                case Direction.Left:
                    player.Draw(spriteBatch, SpriteEffects.FlipHorizontally, 0);
                    break;

                case Direction.Right:
                    player.Draw(spriteBatch, SpriteEffects.None, 0);
                    break;

                case Direction.Up:
                    rotation = MathHelper.ToRadians(-90);
                    player.Draw(spriteBatch, SpriteEffects.None, rotation);
                    break;

                case Direction.Down:
                    rotation = MathHelper.ToRadians(90);
                    player.Draw(spriteBatch, SpriteEffects.None, rotation);
                    break;
            }
        }

        public void DrawLives(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < Game1.lives; index++) 
            {
                spriteBatch.Draw(player.texture, livesPosition[index], null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }

        private void UpdateDirectionInput(KeyboardState keys)
        {
            if (keys.IsKeyDown(Keys.Left)) 
            {
                currentDirection = Direction.Left;
            }
            else if (keys.IsKeyDown(Keys.Right))
            {
                currentDirection= Direction.Right;
            }
            else if (keys.IsKeyDown(Keys.Up)) 
            { 
                currentDirection= Direction.Up; 
            }
            else if (keys.IsKeyDown(Keys.Down)) 
            {
                currentDirection= Direction.Down; 
            }
        }
    }
}
