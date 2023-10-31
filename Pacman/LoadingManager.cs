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

namespace Pacman
{
    public class LoadingManager
    {
        Game game;
        public Level level { get; set; }
        public List<string> stringsFromTextFile { get; private set; }   

        public LoadingManager(Game game)
        {
            this.game = game;

            FillListFromTextFile();
            level = new Level(game, @"tile", stringsFromTextFile);
            var player = new Player(game.Content.Load<Texture2D>(@"player"), level.playerSpawnPosition);
            new PlayerController(player);
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
