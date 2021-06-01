using Cyberpriest.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class MapParser
    {
        Random rand = new Random();
        int random;

        public List<GameObject> objectList;
        public List<GameObject> inventory;
        public Inventory[,] inventoryArray;
        public Background[,] backgroundArray;
        List<PowerUp> powerUpList;
        public List<EnemyType> enemyList;

        public Point tileSize = new Point(64, 64);

        public Player player;
        public Item item;
        public Platform platform;
        public Door door;
        public Background background;
        //public EnemyType enemy;
        public EnemyGhost enemyGhost;
        public Coin coin;
        public Key key;

        public bool doorOpen;

        public PowerUp powerUp;
        public PokemonGeodude geodude;
        public EnemySkeleton enemySkeleton;
        public EnemyRanged enemyRanged;
        public EnemyLust enemyLust;

        Vector2 playerPos;
        Vector2 enemyPos;
        Vector2 wingsPos;
        Vector2 bootsPos;
        Vector2 energyPos;
        Vector2 starPos;
        Vector2 pokemonballPos;
        Vector2 doorPos;
        Vector2 magnetPos;
        Vector2 keyPos;

        Vector2[] enemySkeletonPos;
        Vector2[] enemyRangedPos;
        Vector2[] enemyGhostPos;
        Vector2[] itemPos;
        Vector2[] platformPos;
        Vector2[] backgroundPos;
        Vector2[] coinPos;

        public MapParser(string filename)
        {
            LoadMap(filename);
        }

        public void LoadMap(string fileName)
        {
            objectList = new List<GameObject>();
            inventory = new List<GameObject>();
            inventoryArray = new Inventory[3, 3];
            backgroundArray = new Background[12,12];
            powerUpList = new List<PowerUp>();
            enemyList = new List<EnemyType>();

            List<string> stringList = ReadFromFile(fileName);

            #region Inventory Slots

            for (int i = 0; i < inventoryArray.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryArray.GetLength(1); j++)
                {
                    inventoryArray[i, j] = new Inventory(AssetManager.inventorySlot, new Vector2(64 * i, 64 * j));
                    objectList.Add(inventoryArray[i, j]);
                }
            }

            #endregion

            #region Background
            int backgroundOffset = -2700;

            for (int i = 0; i < backgroundArray.GetLength(0); i++)
            {
                for (int j = 0; j < backgroundArray.GetLength(1); j++)
                {
                    backgroundArray[i, j] = new Background(AssetManager.backgroundLvl1, new Vector2(backgroundOffset + AssetManager.backgroundLvl1.Width * i, backgroundOffset + AssetManager.backgroundLvl1.Height * j));
                }
            }
            #endregion

            #region DGPlatforms

            if (GamePlayManager.levelNumber == 1 || GamePlayManager.levelNumber == 2)
            {
                platformPos = ParseVectorArray(stringList[2]);
                //This is the first platform
                CreatePlatform(AssetManager.longPlatform, platformPos);

                platformPos = ParseVectorArray(stringList[3]);
                //This is the taller version of the platform
                CreatePlatform(AssetManager.tallPlatform, platformPos);

                platformPos = ParseVectorArray(stringList[4]);
                //This is the smallest platform
                CreatePlatform(AssetManager.smallPlatform, platformPos);
            }

            #endregion

            #region BluePlatforms

            if (GamePlayManager.levelNumber == 3 || GamePlayManager.levelNumber == 4)
            {

                platformPos = ParseVectorArray(stringList[2]);
                CreatePlatform(AssetManager.leftTileBlue, platformPos);

                platformPos = ParseVectorArray(stringList[3]);
                CreatePlatform(AssetManager.blockBlue, platformPos);

                platformPos = ParseVectorArray(stringList[4]);
                CreatePlatform(AssetManager.centerTileBlue, platformPos);

                //platformPos = ParseVectorArray(stringList[5]);
                //CreatePlatform(AssetManager.leftLTileBlue, platformPos);

                //platformPos = ParseVectorArray(stringList[6]);
                //CreatePlatform(AssetManager.rightLTileBlue, platformPos);

                platformPos = ParseVectorArray(stringList[7]);
                CreatePlatform(AssetManager.rightTileBlue, platformPos);

                platformPos = ParseVectorArray(stringList[8]);
                CreatePlatform(AssetManager.tallTileBlue, platformPos);
            }

            #endregion

            #region WhitePlatforms

            if (GamePlayManager.levelNumber == 5 || GamePlayManager.levelNumber == 6)
            {
                platformPos = ParseVectorArray(stringList[2]);
                CreatePlatform(AssetManager.leftTileWhite, platformPos);

                platformPos = ParseVectorArray(stringList[3]);
                CreatePlatform(AssetManager.blockWhite, platformPos);

                platformPos = ParseVectorArray(stringList[4]);
                CreatePlatform(AssetManager.centerTileWhite, platformPos);

                //platformPos = ParseVectorArray(stringList[5]);
                //CreatePlatform(AssetManager.leftLTileWhite, platformPos);

                //platformPos = ParseVectorArray(stringList[6]);
                //CreatePlatform(AssetManager.rightLTileWhite, platformPos);

                platformPos = ParseVectorArray(stringList[7]);
                CreatePlatform(AssetManager.rightTileWhite, platformPos);

                platformPos = ParseVectorArray(stringList[8]);
                CreatePlatform(AssetManager.tallTileWhite, platformPos);
            }

            #endregion

            #region Doors

            doorOpen = true;
            doorPos = ParsePos(stringList[10]);
            door = new Door(AssetManager.closedDoor, doorPos, doorOpen);
            objectList.Add(door);

            doorOpen = false;
            doorPos = ParsePos(stringList[11]);
            door = new Door(AssetManager.closedDoor, doorPos, doorOpen);
            objectList.Add(door);

            #endregion

            #region Magnet PowerUp

            magnetPos = ParsePos(stringList[25]);

            powerUp = new PowerUp(AssetManager.magnet, magnetPos);
            powerUpList.Add(powerUp);
            objectList.Add(powerUp);

            #endregion

            #region Energy PowerUp

            energyPos = ParsePos(stringList[13]);

            powerUp = new PowerUp(AssetManager.energy, energyPos);
            powerUpList.Add(powerUp);
            objectList.Add(powerUp);

            #endregion

            #region Star PowerUp

            starPos = ParsePos(stringList[26]);

            powerUp = new PowerUp(AssetManager.star1, starPos);
            powerUpList.Add(powerUp);
            objectList.Add(powerUp);

            #endregion

            #region Key

            keyPos = ParsePos(stringList[27]);

            key = new Key(AssetManager.keySprite, keyPos);
            objectList.Add(key);

            #endregion

            #region Pokemonball PowerUp

            pokemonballPos = ParsePos(stringList[24]);

            powerUp = new PowerUp(AssetManager.pokeball, pokemonballPos);
            powerUpList.Add(powerUp);
            objectList.Add(powerUp);

            #endregion

            #region Player's Start Position

            playerPos = ParsePos(stringList[0]);

            player = new Player(AssetManager.player, playerPos, Game1.window, powerUpList);
            objectList.Add(player);

            #endregion

            #region Geodude PowerUp

            geodude = new PokemonGeodude(AssetManager.pokemonGeodude, new Vector2(player.Position.X - 100, player.Position.Y), powerUp, player, enemyList);
            objectList.Add(geodude);

            #endregion

            #region EnemyGhost

            enemyGhostPos = ParseVectorArray(stringList[12]);
            CreateEnemyGhost(AssetManager.enemyGhost, enemyGhostPos);

            #endregion

            #region EnemySkeleton

            enemySkeletonPos = ParseVectorArray(stringList[13]);
            CreateEnemySkeleton(AssetManager.enemySkeleton, enemySkeletonPos);

            #endregion

            #region EnemyLust

            enemyPos = ParsePos(stringList[14]);

            enemyLust = new EnemyLust(AssetManager.bossCleopatra, enemyPos/*, Game1.window*/, player, geodude);
            enemyList.Add(enemyLust);
            objectList.Add(enemyLust);

            #endregion

            #region EnemyRanged

            enemyRangedPos = ParseVectorArray(stringList[15]);
            CreateEnemyRanged(AssetManager.enemySkeleton, enemyRangedPos);

            #endregion

            #region Item Spawn

            itemPos = ParseVectorArray(stringList[20]);

            for (int i = 0; i < itemPos.Length; i++)
            {
                random = rand.Next(0, 3);
                item = new Item(random, AssetManager.item, itemPos[i], inventoryArray);

                objectList.Add(item);
            }

            #endregion

            #region Wings PowerUp

            wingsPos = ParsePos(stringList[22]);

            powerUp = new PowerUp(AssetManager.wing, wingsPos);
            powerUpList.Add(powerUp);
            objectList.Add(powerUp);

            #endregion

            #region Boots PowerUp

            bootsPos = ParsePos(stringList[23]);

            powerUp = new PowerUp(AssetManager.boots, bootsPos);
            powerUpList.Add(powerUp);
            objectList.Add(powerUp);

            #endregion

            #region Coin

            coinPos = ParseVectorArray(stringList[13]);

            CreateCoin(AssetManager.coinSprite, coinPos);

            #endregion
        }

        public void CreatePlatform(Texture2D texture, Vector2[] pos)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                platform = new Platform(texture, pos[i]);
                objectList.Add(platform);
            }
        }

        public void CreateEnemySkeleton(Texture2D texture, Vector2[] pos)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                enemySkeleton = new EnemySkeleton(texture, pos[i], /*Game1.window,*/ player, geodude);
                objectList.Add(enemySkeleton);
                enemyList.Add(enemySkeleton);
            }
        }

        public void CreateEnemyGhost(Texture2D texture, Vector2[] pos)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                enemyGhost = new EnemyGhost(texture, pos[i], player, geodude);
                objectList.Add(enemyGhost);
                enemyList.Add(enemyGhost);
            }
        }

        public void CreateEnemyRanged(Texture2D texture, Vector2[] pos)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                enemyRanged = new EnemyRanged(texture, pos[i], player, geodude);
                objectList.Add(enemyRanged);
                enemyList.Add(enemyRanged);
            }
        }

        public void CreateCoin(Texture2D texture, Vector2[] pos)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                coin = new Coin(texture, pos[i], player, powerUpList);
                objectList.Add(coin);
            }
        }

        #region Parsers

        public static int ParseInt(string str)
        {
            int x;
            if (!int.TryParse(str, out x))
                Console.WriteLine("Couldn't parse '" + str + "' to int");
            return x;
        }

        public static int[] ParseIntArray(string str)
        {
            string[] stringArray = str.Split(',');
            int[] ints = new int[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                ints[i] = ParseInt(stringArray[i]);
            }
            return ints;
        }

        public static Vector2[] ParseVectorArray(string line)
        {
            // First split into groups = set of 2 integers
            string[] posArray = line.Split('|');
            Vector2[] vectorArray = new Vector2[posArray.Length];

            // Parse each set of 2 integers to a Rectangle
            for (int i = 0; i < posArray.Length; i++)
            {
                vectorArray[i] = ParsePos(posArray[i]);
            }
            return vectorArray;
        }

        public static Vector2 ParsePos(string line)
        {
            int[] intArray = ParseIntArray(line);
            return new Vector2(intArray[0], intArray[1]);
        }

        #endregion

        public List<string> ReadFromFile(string fileName)
        {
            List<string> stringList = new List<string>();

            StreamReader sr = new StreamReader(fileName);

            while (!sr.EndOfStream)
                stringList.Add(sr.ReadLine());

            sr.Close();

            return stringList;
        }

        public void Editor(string fileName)
        {
            //Provides access to local and remote processes and enables you to start and stop local system processes.
            var levelFile = fileName;
            var process = new Process();
            process.StartInfo = new ProcessStartInfo() { FileName = levelFile };

            process.Start();
            process.WaitForExit();
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < backgroundArray.GetLength(0); i++)
            {
                for (int j = 0; j < backgroundArray.GetLength(1); j++)
                {
                    backgroundArray[i, j].Draw(sb);
                }
            }

            foreach (GameObject o in objectList)
                o.Draw(sb);
        }
    }
}

