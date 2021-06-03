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


        public static int levelNumber = 2;

        public static string currentLevel = "level" + levelNumber.ToString();
        public static bool levelComplete;

        static int row;
        static int column;

        public static void Initializer()
        {
            row = 0;
            column = 0;
        }

        public static void MouseRect(GameTime gt)
        {
            mouseRect = new Rectangle(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y, 8, 8);
        }

        public static void Draw(SpriteBatch sb)
        {
            map.Draw(sb);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (GameObject obj in map.objectList)
                obj.Update(gameTime);

            #region Bullet Collision

            foreach (GameObject movingObj in map.objectList)
            {
                if (movingObj is Player)
                {
                    continue;
                }
            }

            foreach (EnemyType enemy in map.enemyList)
            {
                foreach (Bullet bullet in map.player.bulletList)
                {
                    if (bullet.PixelCollision(enemy))
                    {
                        enemy.HandleCollision(bullet);
                    }
                }
            }

            #endregion

            #region Melee

            foreach (GameObject obj in map.objectList)
            {
                if (obj.IntersectCollision(map.player.melee))
                {
                    if (obj is EnemyType)
                    {
                        map.player.melee.HandleCollision(obj);
                        obj.HandleCollision(map.player.melee);
                    }
                }
            }

            #endregion

            #region Player To EnemyBullet

            foreach (GameObject obj in map.objectList)
            {
                foreach (LustBullet eBullet in map.enemyLust.bulletList)
                {
                    if (eBullet.IntersectCollision(obj))
                    {
                        if (eBullet.PixelCollision(obj))
                        {
                            if (obj is Player && eBullet.isActive)
                            {
                                obj.HandleCollision(eBullet);
                            }
                            else
                                continue;
                        }
                    }
                }

                foreach (RangedEnemyBullet eBullet in RangedEnemyBullet.enemyBulletList)
                {
                    if (eBullet is RangedEnemyBullet)
                    {
                        if (obj is Player)
                        {
                            if (obj.PixelCollision(eBullet))
                            {
                                obj.HandleCollision(eBullet);
                                eBullet.HandleCollision(obj);
                            }
                        }

                    }
                }

                foreach (RangedEnemyBullet eBullet in EnemyGreed.greedBulletList)
                {
                    if (eBullet is RangedEnemyBullet)
                    {
                        if (obj is Player)
                        {
                            if (obj.PixelCollision(eBullet))
                            {
                                obj.HandleCollision(eBullet);
                                eBullet.HandleCollision(obj);
                            }
                        }

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
                    if (obj == otherObj)//?
                    {
                        continue;
                    }

                    if (obj.IntersectCollision(otherObj))
                    {
                        #region Platform Collision

                        if (otherObj is Platform)//pixel perfect sudden lag
                        {
                            if (!(obj is Player || obj is EnemyType))
                                continue;

                            //if (otherObj.PixelCollision(obj))
                            //{
                                if (obj is Player || obj is EnemyType)
                                {
                                    int leftSideOffset = 35;
                                    int rightSideOffset = 35;

                                    if (obj.Position.X < (otherObj.Position.X - leftSideOffset) || otherObj.Position.Y < obj.Position.Y || obj.Position.X > otherObj.Position.X + otherObj.GetTexLength - rightSideOffset)
                                        continue;

                                    obj.HandleCollision(otherObj);
                                }
                            //}
                        }

                        #endregion

                        #region Door Collision

                        if (otherObj is Door)
                        {
                            if (otherObj.PixelCollision(obj))
                            {
                                if (obj is Player)
                                {
                                    otherObj.HandleCollision(obj);
                                }
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

                                //if (otherObj.PixelCollision(obj))
                                //{
                                    obj.HandleCollision(otherObj);
                                    otherObj.HandleCollision(obj);
                                //}
                            }
                        }

                        #endregion

                        #region Key Collision

                        if (otherObj is Key)
                        {
                            if (obj is Player)
                            {
                                if (!otherObj.isActive)
                                {
                                    continue;
                                }

                                if (otherObj.PixelCollision(obj))
                                {
                                    otherObj.HandleCollision(obj);
                                }

                            }
                        }

                        #endregion

                        #region Pokemon To Enemy Collision

                        if (obj is PokemonGeodude)
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

                        #region Coin

                        if (otherObj is Coin)
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

        public static void InventoryDraw(SpriteBatch sb)
        {
            int maxWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int maxHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            sb.Draw(AssetManager.inventoryBG, new Rectangle(0, 0, maxWidth, maxHeight), Color.White);

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

