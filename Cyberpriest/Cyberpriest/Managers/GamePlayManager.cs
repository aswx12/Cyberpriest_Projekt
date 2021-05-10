using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class GamePlayManager
    {
        public static MapParser map;
        public static Rectangle mouseRect;

        static int row;
        static int column;

        public static void Initializer()
        {
            map = new MapParser("Content/level1.txt");

            row = 0;
            column = 0;
        }

        public static void Update(GameTime gt)
        {
            mouseRect = new Rectangle(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y, 8, 8);
        }

        public static void Draw(SpriteBatch sb)
        {
            map.Draw(sb);
        }

        public static void CollisionHandler(GameTime gameTime)
        {
            #region Bullet Collision

            foreach (GameObject movingObj in map.objectList)
            {
                if (movingObj is Player)
                {
                    continue;
                }

                foreach (Bullet bullet in map.player.bulletList)
                {
                    if (bullet.IntersectCollision(movingObj))
                    {
                        movingObj.HandleCollision(bullet);
                    }
                }
            }

            #endregion

            foreach (GameObject obj in map.objectList)
                obj.Update(gameTime);

            foreach (GameObject obj in map.objectList)
            {
                foreach (GameObject otherObj in map.objectList)
                {
                    if (otherObj != obj)
                    {
                        if (obj.IntersectCollision(otherObj))
                        {
                            #region Platform Collision

                            if (otherObj is Platform)
                            {
                                if (otherObj.PixelCollision(obj))
                                {
                                    if (obj is Player || obj is EnemyType)
                                    {
                                        int leftSideOffset = 35;
                                        int rightSideOffset = 25;

                                        if (otherObj.GetPos.X > (obj.GetPos.X + leftSideOffset) || otherObj.GetPos.Y < obj.GetPos.Y || (otherObj.GetPos.X + otherObj.GetTexLength - rightSideOffset) < obj.GetPos.X)
                                            continue;
                                    }
                                    obj.HandleCollision(otherObj);
                                }
                            }

                            #endregion

                            #region Player To Enemy Collision

                            if (obj is Player)
                            {
                                if (otherObj is EnemyType)
                                {

                                    if (!otherObj.isActive)
                                        continue;

                                    if (obj.PixelCollision(otherObj))
                                    {
                                        obj.HandleCollision(otherObj);
                                        otherObj.HandleCollision(obj);
                                    }
                                }
                            }

                            #endregion

                            #region Item To Inventory

                            if (otherObj is Item)
                            {
                                if (obj is Player)
                                {
                                    if (map.inventory.Count >= 9)
                                    {
                                        //replace later with text shows on screen instead of debug.
                                        Console.WriteLine("Inventory is full!");
                                        continue;
                                    }

                                    if (!otherObj.isActive)
                                    {
                                        continue;
                                    }

                                    if (otherObj.PixelCollision(obj))
                                    {
                                        (otherObj as Item).row = row;
                                        (otherObj as Item).column = column;
                                        (otherObj as Item).inInventory = true;

                                        if (!map.inventoryArray[row, column].occupied)
                                        {
                                            map.inventory.Add(otherObj);
                                        }

                                        obj.HandleCollision(otherObj);
                                        otherObj.HandleCollision(obj);
                                    }
                                }
                            }

                            #endregion

                            #region PowerUp

                            if (otherObj is PowerUp)
                            {
                                if (obj is Player)
                                {
                                    if (!otherObj.isActive)
                                    {
                                        continue;
                                    }

                                    if (otherObj.PixelCollision(obj))
                                    {
                                        obj.HandleCollision(otherObj);
                                        otherObj.HandleCollision(obj);
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                }
            }
        }

        public static void InventoryDraw(SpriteBatch sb)
        {
            #region Draw Inventory Slots

            for (int i = 0; i < map.inventoryArray.GetLength(0); i++)
            {
                for (int j = 0; j < map.inventoryArray.GetLength(1); j++)
                {
                    map.inventoryArray[i, j].Draw(sb);
                }
            }

            #endregion

            //Draw picked up items in inventory
            foreach (Item item in map.inventory)
                item.DrawInInventory(sb);
        }

        //Usage of items with mouseclicks.
        public static void ItemUse()
        {
            foreach (Inventory inventory in map.inventoryArray)
            {
                if (inventory.GetHitBox.Contains(mouseRect))
                {
                    if (KeyMouseReader.RightClick())
                    {
                        inventory.occupied = false;
                        row = 0;
                        column = 0;
                    }
                }
            }

            foreach (Item item in map.inventory)
            {
                if (item.GetHitBox.Contains(mouseRect) && item.isCollected)
                {
                    if (KeyMouseReader.RightClick())
                    {
                        item.inInventory = false;
                        map.inventory.Remove(item);
                    }
                    break;
                }
            }
        }

        //Check for empty slots in inventory
        public static void InventorySlotCheck()
        {
            if (map.inventoryArray[row, column].occupied)
            {
                row++;
                if (row > 2)
                {
                    if (column < 2)
                        column++;
                    row = 0;
                }
            }
        }

    }
}
