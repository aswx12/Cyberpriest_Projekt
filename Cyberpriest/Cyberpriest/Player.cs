using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cyberpriest
{
    class Player : MovingObject
    {
        private int live;
        public static int score;
        int bY;

        public static Facing playerFacing;
        bool downPlatform;
        Vector2 startPos;

        //public List<Item> inventory = new List<Item>();
        //Item item;

        public Player(Texture2D tex, Vector2 pos, GameWindow window) : base(tex, pos)
        {
            isGrounded = true;
            live = 3;
            startPos = pos;
            vel = new Vector2(0, 0);
            bY = window.ClientBounds.Height;
            isGrounded = true;

            playerFacing = Facing.Idle;

            srRect = new Rectangle(tileSize.X * 0, tileSize.Y * 2, tileSize.X, tileSize.Y);
            //this.inventory = inventory;
            //this.item = item;
        }

        public override void HandleCollision(GameObject other)
        {
            vel.Y = 0;
            isGrounded = true;

            //if (other is Spike)
            //{
            //    live--;
            //    pos = startPos;
            //}

            if (other is Item)
            {
                other.isActive = false;
                return;
            }
            if (downPlatform == true)
            {
                hitBox.Y = other.hitBox.Y - hitBox.Height;
                pos.Y = hitBox.Y;
            }

        }

        public override void Update(GameTime gt)
        {
            if (isGrounded)
                downPlatform = true;

            vel.Y += gravity; //fall speed

            vel.X = 0;

            Control();
            pos += vel;

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);

            //Console.WriteLine(isGrounded);
        }

        public void Control()
        {
            KeyMouseReader.Update();

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Right))
            {
                vel.X = 3;
                effect = SpriteEffects.None;

                playerFacing = Facing.Right;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(Keys.Left))// && pos.X >= startPos.X
            {
                vel.X = -3;
                effect = SpriteEffects.FlipHorizontally;
                playerFacing = Facing.Left;
            }
            else
                playerFacing = Facing.Idle;

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Z)) //add dash cooldown
            {
                if (playerFacing == Facing.Right)
                    pos.X = pos.X + 30;
                if (playerFacing == Facing.Left)
                    pos.X = pos.X - 30;
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Space) && isGrounded)
            {
                vel.Y = -12; //jump height               
                isGrounded = false;
                playerFacing = Facing.Jump;
                srRect = new Rectangle(tileSize.X * 3, tileSize.Y * 0, tileSize.X, tileSize.Y);
            }


            if (KeyMouseReader.keyState.IsKeyDown(Keys.Down) && isGrounded)
            {
                vel.Y = +12;
                isGrounded = false;
                downPlatform = false;
                playerFacing = Facing.Jump;
                srRect = new Rectangle(tileSize.X * 3, tileSize.Y * 0, tileSize.X, tileSize.Y);
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
        }

        public override Vector2 GetPos
        {
            get
            {
                return pos;
            }
        }
    }
}
