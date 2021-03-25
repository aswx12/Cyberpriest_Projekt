using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Cyberpriest
{
    public enum GameState { Start, Play, Over, Menu }
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

        MenuComponent menuComponent;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
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
            window = Window;

            map = new MapParser("level1.txt");

            //gameState = GameState.Start;
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

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboardState = Keyboard.GetState();

            camera.SetPosition(playerPos, gameState);

            switch (gameState)
            {
                case GameState.Start:

                    break;

                case GameState.Play:
                    Console.WriteLine("in play");
                    playerPos = map.player.GetPos;

                    GamePlay(gameTime);

                    break;

                case GameState.Over:

                    break;

                case GameState.Menu:

                    break;
            }
            

            Console.WriteLine(playerPos);

            base.Update(gameTime);
        }

        //Kommentar för test
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            map.Draw(spriteBatch);
            spriteBatch.Draw(AssetManager.bg, new Vector2(-100, -200), Color.White);

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


                            //if (go is Player)
                            //{
                            //    if (other is Enemy)
                            //    {
                            //        if (!other.isActive)
                            //            continue;
                            //        if (go.PixelCollision(other))
                            //        {
                            //            go.HandleCollision(other);
                            //            other.HandleCollision(go);
                            //        }
                            //    }

                            //    if (other is Spike)
                            //    {
                            //        if (go.PixelCollision(other))
                            //        {
                            //            go.HandleCollision(other);
                            //        }
                            //    }

                            //    if (other is Present)
                            //    {
                            //        if (!other.isActive)
                            //            continue;
                            //        if (go.PixelCollision(other))
                            //        {
                            //            go.HandleCollision(other);
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }

            if (keyboardState.IsKeyDown(Keys.M))
            {
                gameState = GameState.Menu;
            }
        }

        //public void Menu()
        //{
        //    if (keyboardState.IsKeyDown(Keys.C))
        //    {
        //        gameState = GameState.Play;
        //    }
        //}
    }
}
