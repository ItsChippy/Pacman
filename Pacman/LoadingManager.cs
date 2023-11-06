using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Pacman
{
    public class LoadingManager
    {
        public Level level { get; set; }
        public List<string> stringsFromTextFile { get; private set; }

        public Song zombieModeSong;
        public SpriteFont zombieFont { get; private set; }
        public SpriteFont gameFont { get; private set; }
        public LoadingManager(Game game)
        {
            zombieModeSong = game.Content.Load<Song>("zombiemode");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
            zombieFont = game.Content.Load<SpriteFont>("zombiemodefont");

            gameFont = game.Content.Load<SpriteFont>("gamefont");
            FillListFromTextFile();
            
            level = new Level(game, @"tile", stringsFromTextFile); //loads the level
            level.LoadLevel();
            
            var player = new Player(game.Content.Load<Texture2D>(@"player"), level.playerSpawnPosition, game.Content); //sets up the player and the player controller
            new PlayerController(player);
            level.GetPlayer(player);

            //instantiates all ghosts with the same Enemy class
            var redGhost = new Enemy(game.Content.Load<Texture2D>(@"redghostsheet"), level.ghostSpawnPosition);
            var blueGhost = new Enemy(game.Content.Load<Texture2D>(@"blueghostsheet"), level.ghostSpawnPosition);
            var pinkGhost = new Enemy(game.Content.Load<Texture2D>(@"pinkghostsheet"), level.ghostSpawnPosition);

            Enemy[] zombieGhostArray = new Enemy[15];
            for (int i = 0; i < zombieGhostArray.Length; i++)
            {
                zombieGhostArray[i] = new Enemy(game.Content.Load<Texture2D>(@"redghostsheet"), level.ghostSpawnPosition);
            }
            new EnemyController(zombieGhostArray, redGhost, blueGhost, pinkGhost, player);
        }

        private void FillListFromTextFile()
        {
            stringsFromTextFile = new List<string>();

            StreamReader sr = new StreamReader("map.txt");
            while (!sr.EndOfStream)
            {
                stringsFromTextFile.Add(sr.ReadLine());
            }
            sr.Close();
        }
    }
}
