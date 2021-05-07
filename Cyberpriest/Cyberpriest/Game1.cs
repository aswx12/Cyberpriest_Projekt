using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Cyberpriest
{
    public enum GameState { Start, Play, Over, Menu, Inventory }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MapParser map;
        Camera camera;
        Vector2 playerPos;
        Rectangle mouseRect;
        MenuComponent menuComponent;
        KeyboardComponent keyboardComponent;

        static GameState gameState;
        public static GameWindow window;
        
        int row = 0;
        int column = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            window = Window;

            menuComponent = new MenuComponent(this);
            keyboardComponent = new KeyboardComponent(this);

            Components.Add(menuComponent);
            Components.Add(keyboardComponent);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = true;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            camera = new Camera(GraphicsDevice.Viewport);

            AssetManager.LoadAssets(Content);
            window.AllowUserResizing = true;
            map = new MapParser("Content/level1.txt");
        }

        public static GameState GetState
        {
            get
            {
                return gameState;
            }

            set
            {
                gameState = value;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseRect = new Rectangle(KeyMouseReader.mouseState.X, KeyMouseReader.mouseState.Y, 8, 8);

            camera.SetPosition(playerPos, gameState);

            UIKeyBinds();

            switch (gameState)
            {
                case GameState.Start:

                    break;

                case GameState.Play:

                    playerPos = map.player.GetPos;

                    InventorySlotCheck();

                    GamePlay(gameTime);

                    break;

                case GameState.Inventory:

                    ItemUse();

                    break;

                case GameState.Over:

                    break;

                case GameState.Menu:

                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            switch (gameState)
            {
                case GameState.Start:

                    menuComponent.Draw(spriteBatch);

                    break;

                case GameState.Play:

                    map.Draw(spriteBatch);

                    spriteBatch.Draw(AssetManager.backgroundLvl1, new Vector2(-100, -200), Color.White);

                    break;

                case GameState.Inventory:

                    //spriteBatch.Draw(AssetManager.inventoryBG, Vector2.Zero, Color.White);
                    DrawInventory(spriteBatch);

                    foreach (Item item in map.inventory) 
                        item.DrawInInventory(spriteBatch);

                    break;

                case GameState.Over:

                    break;

                case GameState.Menu:

                    menuComponent.Draw(spriteBatch);

                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void GamePlay(GameTime gameTime) //skapa en egen klass för gameplay
        {
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
                            if (otherObj is Platform)
                            {
                                if (otherObj.PixelCollision(obj))
                                {
                                    if (obj is Player)
                                    {
                                        int leftSideOffset = 35;
                                        int rightSideOffset = 25;

                                        if (otherObj.GetPos.X > (obj.GetPos.X + leftSideOffset) || otherObj.GetPos.Y < obj.GetPos.Y || (otherObj.GetPos.X + otherObj.GetTexLength - rightSideOffset) < obj.GetPos.X)
                                            continue;
                                    }
                                    obj.HandleCollision(otherObj);
                                }
                            }

                            if (obj is Player)
                            {
                                if (otherObj is EnemyType)
                                {
                                    obj.isHit = true;

                                    if (!otherObj.isActive)
                                        continue;

                                    if (obj.PixelCollision(otherObj))
                                    {
                                        obj.HandleCollision(otherObj);
                                        otherObj.HandleCollision(obj);
                                    }
                                }
                            }

                            #region ItemToInventory

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

                        }
                    }
                }
            }
        }

        #region Methods

        //Keybinds for userinterface
        public void UIKeyBinds()//förklaring kommentar
        {
            if (gameState == GameState.Play)
            {
                if (KeyMouseReader.KeyPressed(Keys.B))
                    gameState = GameState.Inventory;
            }
            else if (gameState == GameState.Inventory)
            {
                if (KeyMouseReader.KeyPressed(Keys.B))
                    gameState = GameState.Play;
            }

            if (gameState == GameState.Play)
            {
                if (KeyMouseReader.KeyPressed(Keys.M))
                    gameState = GameState.Menu;
            }
            else if (gameState == GameState.Menu)
            {
                if (KeyMouseReader.KeyPressed(Keys.M))
                    gameState = GameState.Play;
            }
        }

        public void DrawInventory(SpriteBatch sb)
        {
            for (int i = 0; i < map.inventoryArray.GetLength(0); i++)
            {
                for (int j = 0; j < map.inventoryArray.GetLength(1); j++)
                {
                    map.inventoryArray[i, j].Draw(spriteBatch);
                }
            }
        }

        //Usage of items with mouseclicks.
        public void ItemUse()
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
        public void InventorySlotCheck()
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

        #endregion

    }
}

#region Reusable Code

/*
public void Menu()
{
    if (keyboardState.IsKeyDown(Keys.C))
    {
        gameState = GameState.Play;
    }
}

    
            /*-----------------------------Window Size-------------------------------
graphics.PreferredBackBufferWidth = 1920;
graphics.PreferredBackBufferHeight = 1080;
graphics.ApplyChanges();

public void UnpauseInvent()
        {

            //    gameState = GameState.Inventory;
            //if (KeyMouseReader.KeyPressed(Keys.B) && openInventory == false)
            //{
            //    openInventory = true;
            //    gameState = GameState.Inventory;
            //}
            //else if (KeyMouseReader.KeyPressed(Keys.B) && openInventory == true)
            //{
            //    openInventory = false;
            //    gameState = GameState.Play;
            //} //Active inventory
        }

    /* public void DrawOnRenderTarget(GraphicsDevice device)
         {
             //Ändra så att GraphicsDevice ritar mot vårt render target
             SpriteBatch sb = new SpriteBatch(device);

             device.SetRenderTarget(renderTarget);
             device.Clear(Color.Transparent);
             sb.Begin();

             //Rita ut texturen. Den ritas nu ut till vårt render target istället
             //för på skärmen.
             foreach (Inventory i in invenList)
             {
                 i.Draw(sb);
             }

             sb.End();

             //Sätt GraphicsDevice att åter igen peka på skärmen
             device.SetRenderTarget(null);
         }

                                    //if (other is Spike)
                                //{
                                //    if (go.PixelCollision(other))
                                //    {
                                //        go.HandleCollision(other);
                                //    }
                                //}
















    */

#endregion

