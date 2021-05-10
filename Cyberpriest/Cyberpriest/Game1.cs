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

                    GamePlayManager.Draw(spriteBatch);

                    spriteBatch.Draw(AssetManager.backgroundLvl1, new Vector2(-100, -200), Color.White);

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

