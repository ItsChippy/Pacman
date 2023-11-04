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
        private readonly Player player;

        public EnemyController(Enemy redGhost, Player player)
        {
            this.redGhost = redGhost;
            this.player = player;
            Instance = this;
        }

        public void Update(GameTime gameTime)
        {
            redGhost.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            redGhost.Draw(spriteBatch);
        }
    }
}
