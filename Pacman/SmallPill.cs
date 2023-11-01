using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class SmallPill : BaseGameObject
    {
        public bool isHit {  get; set; }
        SoundEffect sound;
        public SmallPill(Texture2D texture, SoundEffect sound, Vector2 position) :base(texture, position)
        {     
            this.sound = sound;
            isHit = false;
        }

        public void Update(Player player)
        {
            if (!rect.Intersects(player.rect))
            {
                return;
            }
            isHit = true;
            rect.Offset(1000, 1000);
            Game1.AddPoints(100);
            sound.Play(0.1f, 0.0f, 0.0f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isHit)
            {
                spriteBatch.Draw(texture, new Vector2 (position.X - texture.Width / 2 + 16, position.Y - texture.Height / 2 + 16), Color.White);
            }

        }
    }
}
