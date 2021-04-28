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
        private int lives;
        public static int score;
        int bY;
        int normalVel = 3;
        int dashLength = 30;
        int jumpHeight = 12;
        float hitBoxOffset = 0.5f;

        public static Facing playerFacing;
        bool downPlatform;
        Vector2 startPos;

        public Player(Texture2D tex, Vector2 pos, GameWindow window) : base(tex, pos)
        {
            isGrounded = true;
            lives = 3;
            startPos = pos;
            velocity = new Vector2(0, 0);
            bY = window.ClientBounds.Height;
            playerFacing = Facing.Idle;
            srRect = new Rectangle(tileSize.X * 0, tileSize.Y * 2, tileSize.X, tileSize.Y);
        }

        public override void HandleCollision(GameObject other)
        {
            velocity.Y = 0;
            isGrounded = true;

            if (other is EnemyType)
            {
                lives--;
                Console.WriteLine(lives);
                return;
            }

            if (other is Item)
            {
                other.isActive = false;
                (other as Item).isCollected = true;
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
            {
                downPlatform = true;
            }    

            velocity.Y += gravity; //fall speed
            velocity.X = 0;

            Control();
            pos += velocity;

            hitBox.X = (int)(pos.X >= 0 ? pos.X + hitBoxOffset : pos.X - hitBoxOffset);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + hitBoxOffset : pos.Y - hitBoxOffset);
        }

        public void Control()
        {
            KeyMouseReader.Update();

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Right))
            {
                velocity.X = normalVel;
                effect = SpriteEffects.None;

                playerFacing = Facing.Right;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(Keys.Left))// && pos.X >= startPos.X
            {
                velocity.X = -normalVel;
                effect = SpriteEffects.FlipHorizontally;
                playerFacing = Facing.Left;
            }
            else
                playerFacing = Facing.Idle;

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Z)) //add dash cooldown
            {
                if (playerFacing == Facing.Right)
                    pos.X = pos.X + dashLength;
                if (playerFacing == Facing.Left)
                    pos.X = pos.X - dashLength;
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Space) && isGrounded)
            {
                velocity.Y = -jumpHeight; //jump height               
                isGrounded = false;
                playerFacing = Facing.Jump;
                srRect = new Rectangle(tileSize.X * 3, tileSize.Y * 0, tileSize.X, tileSize.Y);
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Down) && isGrounded)
            {
                velocity.Y = jumpHeight;
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

/* Reusabale code
            //if (other is Spike)
            //{
            //    live--;
            //    pos = startPos;
            //}
 
 */
