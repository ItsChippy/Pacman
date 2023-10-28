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
        public List<string> stringsFromTextFile;        
        public Tile[,] tileMap;
        public Texture2D tileTexture;
        

        public LoadingManager(ContentManager content)
        {
            tileTexture = content.Load<Texture2D>(@"tile");

            FillListFromTextFile();
            LoadMap();
        }

        public void LoadMap()
        {
            int numOfRows = stringsFromTextFile[0].Length;
            int numOfCols = stringsFromTextFile.Count;
            tileMap = new Tile[numOfRows, numOfCols];

            for (int row = 0; row < numOfRows; row++) 
            {
                for (int col = 0; col < numOfCols; col++) 
                {
                    switch (stringsFromTextFile[col][row])
                    {
                        case 'X':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Aqua);
                            break;

                        case '-':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Black);
                            break;

                        case '=':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Black);
                            break;

                        case 'R':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Black);
                            break;

                        case 'U':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Green); 
                            break;

                        case 'T':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Turquoise);
                            break;

                        case 'P':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Pink);
                            break;

                        case 'O':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Orange);
                            break;

                        case 'S':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Yellow);
                            break;
                    }
                }
            }
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
