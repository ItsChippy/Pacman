using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Pacman
{
    public class Level
    {
        public Texture2D tileTexture { get; private set; }
        public static Tile[,] tileMap { get; private set; }
        public Vector2 playerSpawnPosition { get; private set; }
        public Vector2 ghostSpawnPosition { get; private set; }
        public Vector2 blueGhostSpawnPosition { get; private set; }

        Game game;
        Player player;

        List<string> stringsFromTextFile;
        int numOfRows;
        int numOfCols;
        public List<SmallPill> smallPills {  get; private set; }

        public Level(Game game, string pathToTexture, List<string> stringsFromTextFile)
        {
            this.game = game;
            smallPills = new List<SmallPill>();
            tileTexture = game.Content.Load<Texture2D>(pathToTexture);
            this.stringsFromTextFile = stringsFromTextFile;

            numOfRows = stringsFromTextFile[0].Length;
            numOfCols = stringsFromTextFile.Count;
            tileMap = new Tile[numOfRows, numOfCols];
        }

        public void LoadLevel()
        {
            SmallPill currentPill;
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
                            currentPill = new SmallPill(game.Content.Load<Texture2D>(@"smallpill"), game.Content.Load<SoundEffect>(@"eatpill"), new Vector2(tileTexture.Width * row, tileTexture.Height * col));
                            smallPills.Add(currentPill);
                            break;

                        case '=':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Black);
                            tileMap[row, col].type = TileType.Empty;
                            break;

                        case 'U':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Green);
                            tileMap[row, col].type = TileType.EnemyGate;
                            break;

                        case 'S':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Black);
                            playerSpawnPosition = new Vector2(tileTexture.Width * row, tileTexture.Height * col);
                            break;

                        case 'R':
                            tileMap[row, col] = new Tile(tileTexture, new Vector2(tileTexture.Width * row, tileTexture.Height * col), Color.Black);
                            ghostSpawnPosition = new Vector2(tileTexture.Width * row, tileTexture.Height * col);
                            break;
                    }
                }
            }
        }

        public void Update()
        {
            for (int index = 0; index < smallPills.Count; index++)
            {
                smallPills[index].Update(player);
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

            for (int index = 0; index < smallPills.Count; index++)
            {
                smallPills[index].Draw(spriteBatch);
            }
        }

        public static TileType CheckTileType(Vector2 tilePosition)
        {
            int tileWidth = tileMap[0, 0].texture.Width;
            int tileHeight = tileMap[0, 0].texture.Height;
            return tileMap[(int)tilePosition.X / tileWidth, (int)tilePosition.Y / tileHeight].type;
        }

        public void GetPlayer(Player player) //injects player into the level
        {
            this.player = player;
        }

        public bool CheckIfWin()
        {
            for (int index = 0; index < smallPills.Count; index++) 
            {
                if (!smallPills[index].isHit)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
