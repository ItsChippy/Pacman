using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public enum TileType
    {
        Empty,
        Wall,
        SmallPill,
        BigPill
    }
    public class Tile : BaseGameObject
    {
        public TileType type;
        Color color;
        public Tile(Texture2D texture, Vector2 position, Color color) : base(texture, position)
        {
            this.color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }

}
