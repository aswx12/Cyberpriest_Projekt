﻿using Microsoft.Xna.Framework;
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
        public Point tileSize = new Point(64, 64);

        public Player player;
        public Item item;
        public Platform platform;

        Vector2 PlayerPos;
        Vector2[] itemPos;
        Vector2[] platformPos;

        public Vector2 slotPos;

        //public int row = 0;
        //public int column = 0;

        public MapParser(string filename)
        {
            LoadMap(filename);
        }

        public void LoadMap(string fileName)
        {

            objectList = new List<GameObject>();
            inventory = new List<GameObject>();
            inventoryArray = new Inventory[3, 3];

            List<string> stringList = ReadFromFile(fileName);
           
            /*-----------------------Inventory slots-----------------*/
            for (int i = 0; i < inventoryArray.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryArray.GetLength(1); j++)
                {
                    inventoryArray[i, j] = new Inventory(AssetManager.walltile, new Vector2(64 * i + 200, 64 * j+200));
                }
            }
            
            /*--------------------Map--------------------------*/


            platformPos = ParseVectorArray(stringList[2]);

            for (int i = 0; i < platformPos.Length; i++)
            {
                platform = new Platform(AssetManager.platform, platformPos[i]);
                objectList.Add(platform);

            }

            itemPos = ParseVectorArray(stringList[5]);

            for (int i = 0; i < itemPos.Length; i++)
            {
                random = rand.Next(0, 3);
                item = new Item(random,AssetManager.item, itemPos[i],inventoryArray);

                objectList.Add(item);
            }

            PlayerPos = ParsePos(stringList[0]);

            player = new Player(AssetManager.player, PlayerPos, Game1.window);
            objectList.Add(player);

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

