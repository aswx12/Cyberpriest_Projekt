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
        List<PowerUp> powerUpList;

        public Point tileSize = new Point(64, 64);

        public Player player;
        public Item item;
        public Platform platform;
        //public EnemyType enemy;
        public EnemyGhost enemyGhost;
        public Coin coin;

        public PowerUp powerUp;
        public EnemySkeleton enemySkeleton;
        public EnemyLust enemyLust;

        Vector2 PlayerPos;
        Vector2 EnemyPos;
        Vector2 WingsPos;
        Vector2 BootsPos;
        Vector2 MagnetPos;
        Vector2[] enemySkeletonPos;
        Vector2[] itemPos;
        Vector2[] platformPos;
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
            powerUpList = new List<PowerUp>();

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

            #region Platforms

            platformPos = ParseVectorArray(stringList[2]);

            //This is the first platform
            CreatePlatform(AssetManager.longPlatform, platformPos);

            platformPos = ParseVectorArray(stringList[7]);

            //This is the taller version of the platform
            CreatePlatform(AssetManager.tallPlatform, platformPos);

            platformPos = ParseVectorArray(stringList[8]);

            //This is the smallest platform
            CreatePlatform(AssetManager.smallPlatform, platformPos);

            #endregion

            #region Item Spawn

            itemPos = ParseVectorArray(stringList[5]);

            for (int i = 0; i < itemPos.Length; i++)
            {
                random = rand.Next(0, 3);
                item = new Item(random, AssetManager.item, itemPos[i], inventoryArray);

                objectList.Add(item);
            }

            #endregion

            #region Wings PowerUp

            WingsPos = ParsePos(stringList[11]);

            powerUp = new PowerUp(AssetManager.wing, WingsPos);
            powerUpList.Add(powerUp);
            objectList.Add(powerUp);

            #endregion

            #region Boots PowerUp

            BootsPos = ParsePos(stringList[12]);

            powerUp = new PowerUp(AssetManager.boots, BootsPos);
            powerUpList.Add(powerUp);
            objectList.Add(powerUp);

            #endregion

            #region Magnet PowerUp

            MagnetPos = ParsePos(stringList[14]);

            powerUp = new PowerUp(AssetManager.magnet, MagnetPos);
            powerUpList.Add(powerUp);
            objectList.Add(powerUp);

            #endregion

            #region Player's Start Position

            PlayerPos = ParsePos(stringList[0]);

            player = new Player(AssetManager.player, PlayerPos, Game1.window, powerUpList);
            objectList.Add(player);

            #endregion

            #region EnemyGhost

            EnemyPos = ParsePos(stringList[6]);

            enemyGhost = new EnemyGhost(AssetManager.enemyGhost, EnemyPos/*, Game1.window*/, player);
            objectList.Add(enemyGhost);

            #endregion

            #region EnemySkeleton

            enemySkeletonPos = ParseVectorArray(stringList[9]);

            CreateEnemySkeleton(AssetManager.enemySkeleton, enemySkeletonPos);

            //EnemyPos = ParsePos(stringList[9]);

            //enemySkeleton = new EnemySkeleton(AssetManager.enemySkeleton, EnemyPos/*, Game1.window*/, player);
            //objectList.Add(enemySkeleton);

            #endregion

            #region EnemyLust

            EnemyPos = ParsePos(stringList[10]);

            enemyLust = new EnemyLust(AssetManager.bossCleopatra, EnemyPos/*, Game1.window*/, player);
            objectList.Add(enemyLust);

            #endregion

            #region Coin

            coinPos = ParseVectorArray(stringList[13]);

            CreateCoin(AssetManager.avocado, coinPos);

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
                enemySkeleton = new EnemySkeleton(texture, pos[i], /*Game1.window,*/ player);
                objectList.Add(enemySkeleton);
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
            foreach (GameObject o in objectList)
                o.Draw(sb);
        }
    }
}

