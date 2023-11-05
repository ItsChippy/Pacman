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
    public sealed class EnemyController
    {


        public static EnemyController Instance { get; private set; }
        
        private readonly Enemy redGhost;
        private readonly Enemy blueGhost;
        private readonly Enemy pinkGhost;
        private readonly Player player;

        public EnemyController(Enemy redGhost, Enemy blueGhost, Enemy pinkGhost, Player player)
        {
            this.redGhost = redGhost;
            this.blueGhost = blueGhost;
            this.pinkGhost = pinkGhost;
            this.player = player;
            Instance = this;
        }

        public void Update(GameTime gameTime)
        {
            redGhost.Update(player, gameTime);
            blueGhost.Update(player, gameTime);
            pinkGhost.Update(player, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            redGhost.Draw(spriteBatch);
            blueGhost.Draw(spriteBatch);
            pinkGhost.Draw(spriteBatch);
        }
    }
}
