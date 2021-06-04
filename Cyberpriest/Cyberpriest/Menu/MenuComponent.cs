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

        bool continueEnabled;

        public MenuComponent(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            continueEnabled = false;
            choices = new List<MenuChoice>();
            choices.Add(new MenuChoice() { Text = "NEW GAME", Selected = true, ClickAction = MenuStartClicked });

            choices.Add(new MenuChoice() { Text = "OPTIONS", ClickAction = MenuOptionsClicked });
            choices.Add(new MenuChoice() { Text = "QUIT", ClickAction = MenuQuitClicked });


            base.Initialize();
        }

        public void ContinueEnable()
        {
            if (Player.dead)
            {
                continueEnabled = false;
            }

            if (!Player.dead)
            {
                choices[1] = (new MenuChoice() { Text = "CONTINUE", ClickAction = ContinueClicked });
                choices[2] = (new MenuChoice() { Text = "OPTIONS", ClickAction = MenuOptionsClicked });

                if (!continueEnabled)
                {
                    continueEnabled = true;
                    choices.Add(new MenuChoice() { Text = "QUIT", ClickAction = MenuQuitClicked });
                }
            }
            else
            {
                choices[1] = (new MenuChoice() { Text = "OPTIONS", ClickAction = MenuOptionsClicked });
                choices[2] = (new MenuChoice() { Text = "QUIT", ClickAction = MenuQuitClicked });
                if (choices.Count >= 4)
                    choices.RemoveAt(3);

            }

        }

        #region Menu Clicks

        private void MenuStartClicked()
        {
            GamePlayManager.levelNumber = 1;
            GamePlayManager.currentLevel = "level" + GamePlayManager.levelNumber.ToString();
            GamePlayManager.map = new MapParser("Content/" + GamePlayManager.currentLevel + ".txt");
            Game1.GetState = GameState.Play;
            Game1.newGame = true;        
        }

        private void ContinueClicked()
        {
            Game1.GetState = GameState.Play;
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

            previousMouseState = Mouse.GetState();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.GetState == GameState.Menu)
            {
                if (KeyboardComponent.KeyPressed(Keys.S))
                    NextMenuChoice();
                if (KeyboardComponent.KeyPressed(Keys.W))
                    PreviousMenuChoice();
                if (KeyboardComponent.KeyPressed(Keys.Enter))
                {
                    try
                    {
                        var selectedChoice = choices.First(c => c.Selected);
                        selectedChoice.ClickAction.Invoke();
                    }
                    catch { }
                }


                int choicesGap = 70;
                float startY = Game1.window.ClientBounds.Height / 4f;

                foreach (var choice in choices)
                {
                    Vector2 size = AssetManager.normalFont.MeasureString(choice.Text);
                    choice.posY = startY;
                    choice.posX = Game1.window.ClientBounds.Width / 2 - size.X / 2;
                    choice.HitBox = new Rectangle((int)choice.posX, (int)choice.posY, (int)size.X, (int)size.Y);
                    startY += choicesGap;
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
                    choice.Text, new Vector2(choice.posX, choice.posY), Color.White);
            }
        }
    }
}
