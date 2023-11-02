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
    public class BaseGhost
    {
        Texture2D texture;
        Vector2 position;
        public Rectangle rect;

        public BaseGhost(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public virtual void Move(GameTime gameTime)
        {

        }

        protected void UpdateRectanglePos()
        {
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
        }
    }
}
