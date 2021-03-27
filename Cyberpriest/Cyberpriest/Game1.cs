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
        bool openInventory;

        MenuComponent menuComponent;
        public RenderTarget2D renderTarget;
        public static Vector2 inventorySlotPos, previousSlot;

        public int row = 0;
        public int column = 0;
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
            openInventory = false;
            window.AllowUserResizing = true;
            map = new MapParser("level1.txt");

            //invenList = new List<Inventory>();
            //inventory = new Inventory(AssetManager.inventory, new Vector2(100, 100));
            //renderTarget = renderTarget = new RenderTarget2D(GraphicsDevice, window.ClientBounds.Width, window.ClientBounds.Height);
            //gameState = GameState.Start;

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

            camera.SetPosition(playerPos, gameState);
            UIKeyBinds();
            //inventorySlotPos = map.inventoryArray[row, column].GetSlotPos;
            //previousSlot = inventorySlotPos;//to save previous item location? 
            switch (gameState)
            {
                case GameState.Start:

                    break;

                case GameState.Play:

                    playerPos = map.player.GetPos;

                    GamePlay(gameTime);

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

                    break;

                case GameState.Inventory:

                    break;

                case GameState.Over:

                    break;

                case GameState.Menu:

                    break;
            }

            //Console.WriteLine(map.inventory.Count);

            base.Update(gameTime);
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
         }*/

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Start:
                    menuComponent.Draw(spriteBatch);
                    break;

                case GameState.Play:
                    map.Draw(spriteBatch);
                    spriteBatch.Draw(AssetManager.bg, new Vector2(-100, -200), Color.White);
                    // if(openInventory)
                    //spriteBatch.Draw(AssetManager.inventory, Vector2.Zero, Color.White);

                    DrawInventory(spriteBatch);

                    foreach (Item item in map.inventory) //live inventory update
                        item.DrawInInventory(spriteBatch,row,column);
                    break;

                case GameState.Inventory:
                    spriteBatch.Draw(AssetManager.inventory, Vector2.Zero, Color.White);

                    DrawInventory(spriteBatch);
                    //foreach (Item item in map.inventory)
                    //    item.DrawInInventory(spriteBatch);

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

                            if (other is Item)
                            {
                                if (go is Player)
                                {
                                    if (!other.isActive)
                                    {
                                        continue;
                                    }
                                    /*-----------------------------CHECK HERE-----------------------*/
                                    if (go.PixelCollision(other))
                                    {
                                        //foreach (Item item in map.inventory)
                                        //    map.item.SetSlotPos = inventorySlotPos;
                                        //Item item = new Item(map.item.GetTexture, inventorySlotPos,map.inventory,map.inventoryArray);
                                        //foreach(Item item in map.inventory)
                                        
                                        map.inventory.Add(other);
                                        if(map.inventory.Count > 1)
                                        {
                                            row++;
                                            if (row >= 3)
                                            {
                                                column++;
                                                row = 0;
                                            }
                                        }
                                        go.HandleCollision(other);

                                    }
                                }                           
                                    
                            }
                        }
                    }
                }
            }
        }
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















    */


