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
        int dashLength = 150;
        int jumpHeight = 12;
        int dashCount;
        double dashCD;
        float hitBoxOffset = 0.5f;

        public static Facing playerFacing;
        bool onPlatform;
        public bool test;
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

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);

            dashCount = 1;
            dashCD = 0;

            test = false;
        }

        public override void HandleCollision(GameObject other)
        {
            if (other is Platform)
            {
                velocity.Y = 0;
                isGrounded = true;
                if (hitBox.Y >= other.GetPos.Y)
                {
                    test = true;
                }
            }

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

            if (onPlatform == true)
            {
                if (test)
                    return;
                hitBox.Y = other.hitBox.Y - hitBox.Height;
                pos.Y = hitBox.Y;
            }

           
        }

        public override void Update(GameTime gt)
        {
            Console.WriteLine(pos);
            if (isGrounded)
            {
                onPlatform = true;
            }

            //fall speed
            velocity.Y += gravity; 
            velocity.X = 0;

            Control();
            DashCooldown(gt);

            pos += velocity;
            hitBox.X = (int)(pos.X >= 0 ? pos.X + hitBoxOffset : pos.X - hitBoxOffset);

            //the platform issue lays here?
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
                if (dashCount > 0)
                {
                    if (playerFacing == Facing.Right)
                        velocity.X += dashLength;
                    if (playerFacing == Facing.Left)
                        velocity.X -= dashLength;

                    dashCount = 0;
                }
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
                onPlatform = false;
                playerFacing = Facing.Jump;
                srRect = new Rectangle(tileSize.X * 3, tileSize.Y * 0, tileSize.X, tileSize.Y);
                test = true;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
        }

        public void DashCooldown(GameTime gt)
        {
            if (dashCount <= 0)
                dashCD += gt.ElapsedGameTime.TotalSeconds;

            double cooldown = 1.5;

            if (dashCD >= cooldown && dashCount == 0)
            {
                dashCount = 1;
                if (dashCount == 1)
                    dashCD = 0;
            }
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
