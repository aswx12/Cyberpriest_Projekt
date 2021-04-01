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
        public List<GameObject> objectList;

        Point tileSize = new Point(50, 50);

        public Player player;
        public Platform platform;
        public EnemyType enemy;

       
        Vector2 PlayerPos;
<<<<<<< Updated upstream
=======
        Vector2 EnemyPos;
        Vector2[] itemPos;
>>>>>>> Stashed changes
        Vector2[] platformPos;

        public MapParser(string filename)
        {
            LoadMap(filename);
        }

        public void LoadMap(string fileName)
        {

            objectList = new List<GameObject>();

            List<string> stringList = ReadFromFile(fileName);
<<<<<<< Updated upstream

            /*--------------------Map--------------------------*/
            PlayerPos = ParsePos(stringList[0]);

            player = new Player(AssetManager.player, PlayerPos, Game1.window);
            objectList.Add(player);
=======
           
            /*-----------------------Inventory slots-----------------*/
            for (int i = 0; i < inventoryArray.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryArray.GetLength(1); j++)
                {
                    inventoryArray[i, j] = new Inventory(AssetManager.walltile, new Vector2(64 * i + 200, 64 * j+200));
                }
            }

            /*--------------------Enemy------------------------*/

            EnemyPos = ParsePos(stringList[6]);

            enemy = new EnemyType(AssetManager.enemy, EnemyPos, Game1.window);
            objectList.Add(enemy);
            
            /*--------------------Map--------------------------*/
         
>>>>>>> Stashed changes

            platformPos = ParseVectorArray(stringList[2]);

            for (int i = 0; i < platformPos.Length; i++)
            {
                platform = new Platform(AssetManager.platform, platformPos[i]);
                objectList.Add(platform);

            }

        }


        /*--------------------PARSERS-------------------*/

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

        /*--------------------------------------------------*/

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

