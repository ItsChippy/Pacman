using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman
{
    public class Level
    {
        public Texture2D tileTexture { get; private set; }
        public static Tile[,] tileMap { get; private set; }
        public Vector2 playerSpawnPosition { get; private set; }

        List<string> stringsFromTextFile;

        public Level(Game game, string pathToTexture, List<string> stringsFromTextFile)
        {
            tileTexture = game.Content.Load<Texture2D>(pathToTexture);
            this.stringsFromTextFile = stringsFromTextFile;

            int numOfRows = stringsFromTextFile[0].Length;
            int numOfCols = stringsFromTextFile.Count;
            tileMap = new Tile[numOfRows, numOfCols];
            LoadLevel(numOfRows, numOfCols);
        }

        private void LoadLevel(int numOfRows, int numOfCols)
        {
            for (int row = 0; row < numOfRows; row++)
            {
                for (int col = 0; col < numOfCols; col++)
                {
                    switch (stringsFromTextFile[col][row])
                    {
                        case 'X':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.DarkCyan);
                            tileMap[row, col].type = TileType.Wall;
                            break;

                        case '-':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Black);
                            tileMap[row, col].type = TileType.SmallPill;
                            break;

                        case '=':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Black);
                            tileMap[row, col].type = TileType.Empty;
                            break;

                        case 'U':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Green);
                            tileMap[row, col].type = TileType.Empty;
                            break;

                        case 'S':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Black);
                            playerSpawnPosition = new Vector2(tileTexture.Width * row, tileTexture.Height * col);
                            break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int rows = 0; rows < tileMap.GetLength(0); rows++)
            {
                for (int cols = 0; cols < tileMap.GetLength(1); cols++)
                {
                    tileMap[rows, cols].Draw(spriteBatch);
                }
            }
        }

        public static TileType CheckTileType(Vector2 tilePosition)
        {
            int tileWidth = tileMap[0, 0].texture.Width;
            int tileHeight = tileMap[0, 0].texture.Height;
            return tileMap[(int)tilePosition.X / tileWidth, (int)tilePosition.Y / tileHeight].type;
        }
    }
}
