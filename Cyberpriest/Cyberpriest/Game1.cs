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

        static GameState gameState;
        KeyboardState keyboardState;

        public static GameWindow window;
        Vector2 playerPos;
        //bool openInventory;

        MenuComponent menuComponent;
        public RenderTarget2D renderTarget;
        public Rectangle mouseRect;
        Vector2 emptySlot;
        //public MouseState mouseState, previousMouseState;

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
            Components.Add(menuComponent);
            Components.Add(new KeyboardComponent(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {

            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            camera = new Camera(GraphicsDevice.Viewport);
            AssetManager.LoadAssets(Content);
            window.AllowUserResizing = true;
            map = new MapParser("level1.txt");

            //renderTarget = renderTarget = new RenderTarget2D(GraphicsDevice, window.ClientBounds.Width, window.ClientBounds.Height);
            //openInventory = false;

            /*-----------------------------Window Size-------------------------------*/
            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;
            //graphics.ApplyChanges();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboardState = Keyboard.GetState();
            KeyMouseReader.Update();

            //previousMouseState = mouseState;
            //mouseState = Mouse.GetState();
            mouseRect = new Rectangle(KeyMouseReader.mouseState.X - 8, KeyMouseReader.mouseState.Y - 8, 8, 8);



            camera.SetPosition(playerPos, gameState);
            UIKeyBinds();

            switch (gameState)
            {
                case GameState.Start:

                    break;

                case GameState.Play:

                    playerPos = map.player.GetPos;

                    GamePlay(gameTime);



                    break;

                case GameState.Inventory:

                    //if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    //{
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
                    spriteBatch.Draw(AssetManager.bg, new Vector2(-100, -200), Color.White);

                    #region Unpaused Invetory If Needed
                    // if(openInventory)
                    //spriteBatch.Draw(AssetManager.inventory, Vector2.Zero, Color.White);
                    #endregion


                    break;

                case GameState.Inventory:
                    spriteBatch.Draw(AssetManager.inventory, Vector2.Zero, Color.White);
                    DrawInventory(spriteBatch);
                    foreach (Item item in map.inventory) //live inventory update
                        if (item.isCollected)
                            item.DrawInInventory(spriteBatch);//, item.row, item.column);

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

        public void GamePlay(GameTime gameTime)
        {
            foreach (GameObject go in map.objectList)
                go.Update(gameTime);

            foreach (GameObject go in map.objectList)
            {
                foreach (GameObject other in map.objectList)
                {
                    if (other != go)
                    {
                        if (go.IntersectCollision(other))
                        {
                            if (other is Platform)
                            {
                                other.PixelCollision(go);
                                go.HandleCollision(other);
                            }

                            /* if (go is Player)
                             {
                                 if (other is Enemy)
                                 {
                                     if (!other.isActive)
                                         continue;
                                     if (go.PixelCollision(other))
                                     {
                                         go.HandleCollision(other);
                                         other.HandleCollision(go);
                                     }
                                 }

                                 if (other is Spike)
                                 {
                                     if (go.PixelCollision(other))
                                     {
                                         go.HandleCollision(other);
                                     }
                                 }*/

                            #region ItemToInventory
                            if (other is Item)
                            {
                                if (go is Inventory)
                                {

                                    go.HandleCollision(other); //?

                                }

                                if (go is Player)
                                {
                                    if (!other.isActive)
                                    {
                                        continue;
                                    }

                                    if (go.PixelCollision(other))
                                    {
                                        if (!(map.inventoryArray[row, column].empty))
                                        {
                                            row++;
                                            if (row >= 3)
                                            {
                                                column++;
                                                row = 0;
                                            }
                                            (other as Item).row = row;
                                            (other as Item).column = column;
                                        }

                                        if (map.inventoryArray[row, column].empty)//
                                        {
                                            map.inventory.Add(other);
                                        }

                                        go.HandleCollision(other);

                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
        }

        /*--------------------------METHODS--------------------------*/

        public void UIKeyBinds()
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

        public void ItemUse()
        {
            foreach (Item item in map.inventory)
            {
                foreach (Inventory inventory in map.inventoryArray)
                {
                    if (item.getHitbox.Contains(mouseRect) && item.isCollected)
                    {
                        if (KeyMouseReader.RightClick())
                        {
                            map.inventory.Remove(item);//item.isCollected = false;



                        }
                        break;
                    }

                    if (inventory.getHitbox.Contains(mouseRect) && inventory.empty == true)
                    {
                        if (KeyMouseReader.RightClick())
                        {

                            Console.WriteLine("Mouse: " + mouseRect);
                            Console.WriteLine("invhitbox: " + inventory.getHitbox);
                        }

                    }
                }
            }

            //foreach (Inventory inventory in map.inventoryArray)
            //{
            //    if (inventory.getHitbox.Contains(mouseRect) && inventory.empty == true)
            //    {
            //        if (KeyMouseReader.RightClick())
            //        {

            //            Console.WriteLine("Mouse: " + mouseRect);
            //            Console.WriteLine("invhitbox: " + inventory.getHitbox);
            //        }

            //    }
            //}



        }
    }
}



/*------------------TRASH-----------------------------------------------------
public void Menu()
{
    if (keyboardState.IsKeyDown(Keys.C))
    {
        gameState = GameState.Play;
    }
}

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
















    */


