using Cyberpriest.Menu;
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

        Camera camera;
        Vector2 playerPos;
        MenuComponent menuComponent;
        KeyboardComponent keyboardComponent;
        Background[,] bgArray;
        Background[,] bgArray2;
        Background[,] bgArray3;
        Background[,] bgArray4;

        static GameState gameState;
        public static GameWindow window;

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

            GamePlayManager.Initializer();

            AssetManager.LoadAssets(Content);

            window.AllowUserResizing = true;

            GamePlayManager.map = new MapParser("Content/level1.txt");

            #region Background (MÅSTE FIXAS)
            bgArray = new Background[9, 9];
            bgArray2 = new Background[9, 9];
            bgArray3 = new Background[9, 9];
            bgArray4 = new Background[9, 9];

            for (int i = 0; i < bgArray.GetLength(0); i++)
            {
                for (int j = 0; j < bgArray.GetLength(1); j++)
                {
                    int posX = j * AssetManager.backgroundLvl1.Width;
                    int posY = i * AssetManager.backgroundLvl1.Height;
                    bgArray[i, j] = new Background(AssetManager.backgroundLvl1, posX, posY);
                }
            }

            for (int i = 0; i < bgArray2.GetLength(0); i++)
            {
                for (int j = 0; j < bgArray2.GetLength(1); j++)
                {
                    int posX = -(j * AssetManager.backgroundLvl1.Width);
                    int posY = -(i * AssetManager.backgroundLvl1.Height);
                    bgArray2[i, j] = new Background(AssetManager.backgroundLvl1, posX, posY);
                }
            }

            for (int i = 0; i < bgArray3.GetLength(0); i++)
            {
                for (int j = 0; j < bgArray3.GetLength(1); j++)
                {
                    int posX = -(j * AssetManager.backgroundLvl1.Width);
                    int posY = i * AssetManager.backgroundLvl1.Height;
                    bgArray3[i, j] = new Background(AssetManager.backgroundLvl1, posX, posY);
                }
            }

            for (int i = 0; i < bgArray4.GetLength(0); i++)
            {
                for (int j = 0; j < bgArray4.GetLength(1); j++)
                {
                    int posX = j * AssetManager.backgroundLvl1.Width;
                    int posY = -(i * AssetManager.backgroundLvl1.Height);
                    bgArray4[i, j] = new Background(AssetManager.backgroundLvl1, posX, posY);
                }
            }
            #endregion
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

            GamePlayManager.Update(gameTime);

            camera.SetPosition(playerPos, gameState);

            UIKeyBinds();

            switch (gameState)
            {
                case GameState.Start:

                    break;

                case GameState.Play:

                    playerPos = GamePlayManager.map.player.GetPos;

                    GamePlayManager.InventorySlotCheck();

                    GamePlayManager.CollisionHandler(gameTime);

                    break;

                case GameState.Inventory:

                    GamePlayManager.ItemUse();

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
                    #region BackgroundDraw (MÅSTE FIXAS)
                    foreach (Background background in bgArray)
                    {
                        background.Draw(spriteBatch);
                    }
                    foreach (Background background in bgArray2)
                    {
                        background.Draw(spriteBatch);
                    }
                    foreach (Background background in bgArray3)
                    {
                        background.Draw(spriteBatch);
                    }
                    foreach (Background background in bgArray4)
                    {
                        background.Draw(spriteBatch);
                    }
                    #endregion

                    GamePlayManager.Draw(spriteBatch);
                    
                    break;

                case GameState.Inventory:

                    GamePlayManager.InventoryDraw(spriteBatch);

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

