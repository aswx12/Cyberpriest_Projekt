using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        List<MenuChoice> choices;
        Color bgColor; // to simulate "States"
        MouseState previousMouseState;
        //GameState gameState;

        public MenuComponent(Game game)
            : base(game)
        {
            //this.gameState = gameState;
        }

        //public GameState GetState
        //{
        //    get
        //    {
        //        return gameState;
        //    }

        //    set
        //    {
        //        gameState = value;
        //    }
        //}


        public override void Initialize()
        {
            choices = new List<MenuChoice>();
            choices.Add(new MenuChoice() { Text = "START", Selected = true, ClickAction = MenuStartClicked });
            choices.Add(new MenuChoice() { Text = "SELECT LEVEL", ClickAction = MenuSelectClicked });
            choices.Add(new MenuChoice() { Text = "OPTIONS", ClickAction = MenuOptionsClicked });
            choices.Add(new MenuChoice() { Text = "QUIT", ClickAction = MenuQuitClicked });

            base.Initialize();
        }

        #region Menu Clicks

        private void MenuStartClicked()
        {
            Game1.GetState = GameState.Play;
        }

        private void MenuSelectClicked()
        {
            bgColor = Color.Teal;
        }

        private void MenuOptionsClicked()
        {
            bgColor = Color.Silver;
        }

        private void MenuQuitClicked()
        {
            this.Game.Exit();
        }

        #endregion

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bgColor = Color.Black;
            AssetManager.LoadAssets(Game.Content);
            int choicesGap = 70;
            float startY = 0.5f * GraphicsDevice.Viewport.Height;

            foreach (var choice in choices)
            {
                Vector2 size = AssetManager.normalFont.MeasureString(choice.Text);
                choice.Y = startY;
                choice.X = GraphicsDevice.Viewport.Width / 0.85f - size.X / 2;
                choice.HitBox = new Rectangle((int)choice.X, (int)choice.Y, (int)size.X, (int)size.Y);
                startY += choicesGap;
            }

            previousMouseState = Mouse.GetState();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (KeyboardComponent.KeyPressed(Keys.Down))
                NextMenuChoice();
            if (KeyboardComponent.KeyPressed(Keys.Up))
                PreviousMenuChoice();
            if (KeyboardComponent.KeyPressed(Keys.Enter))
            {
                var selectedChoice = choices.First(c => c.Selected);
                selectedChoice.ClickAction.Invoke();
            }

            var mouseState = Mouse.GetState();

            foreach (var choice in choices)
            {
                if (choice.HitBox.Contains(mouseState.X, mouseState.Y))
                {
                    choices.ForEach(c => c.Selected = false);
                    choice.Selected = true;

                    if (previousMouseState.LeftButton == ButtonState.Released
                        && mouseState.LeftButton == ButtonState.Pressed)
                        choice.ClickAction.Invoke();
                }
            }

            previousMouseState = mouseState;

            base.Update(gameTime);
        }

        private void PreviousMenuChoice()
        {
            int selectedIndex = choices.IndexOf(choices.First(c => c.Selected));
            choices[selectedIndex].Selected = false;
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = choices.Count - 1;
            choices[selectedIndex].Selected = true;
        }

        private void NextMenuChoice()
        {
            int selectedIndex = choices.IndexOf(choices.First(c => c.Selected));
            choices[selectedIndex].Selected = false;
            selectedIndex++;
            if (selectedIndex >= choices.Count)
                selectedIndex = 0;
            choices[selectedIndex].Selected = true;
        }

        public void Draw(SpriteBatch sb)
        {
            GraphicsDevice.Clear(bgColor);

            foreach (var choice in choices)
            {
                sb.DrawString(choice.Selected ? AssetManager.selectedFont : AssetManager.normalFont,
                    choice.Text, new Vector2(choice.X, choice.Y), Color.White);
            }

        }

    }
}
