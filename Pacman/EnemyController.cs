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
        private readonly Enemy[] zombieGhosts;
        private readonly Player player;

        public EnemyController(Enemy[] zombieGhosts, Enemy redGhost, Enemy blueGhost, Enemy pinkGhost, Player player)
        {
            this.redGhost = redGhost;
            this.blueGhost = blueGhost;
            this.pinkGhost = pinkGhost;
            this.player = player;
            this.zombieGhosts = zombieGhosts;
            Instance = this;
        }

        public void Update(GameTime gameTime)
        {
            redGhost.Update(player, gameTime);
            blueGhost.Update(player, gameTime);
            pinkGhost.Update(player, gameTime);
        }

        public void UpdateZombies(GameTime gameTime)
        {
            for (int i = 0; i < zombieGhosts.Length; i++) 
            {
                zombieGhosts[i].Update(player, gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            redGhost.Draw(spriteBatch, Color.White);
            blueGhost.Draw(spriteBatch, Color.White);
            pinkGhost.Draw(spriteBatch, Color.White);
        }

        public void DrawZombies(SpriteBatch spriteBatch) 
        {
            for (int i = 0; i < zombieGhosts.Length; i++)
            {
                zombieGhosts[i].Draw(spriteBatch, Color.LightGreen);
            }
        }
    }
}
