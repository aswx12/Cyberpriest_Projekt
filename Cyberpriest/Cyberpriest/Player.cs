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

        public List<Bullet> bulletList = new List<Bullet>();
        List<PowerUp> powerUpList;
        Vector2 startPos;
        Bullet bullet;

        public Facing playerFacing;

        //private int lives;
        int bY, bX;
        int normalVel = 3;
        int dashLength = 150;
        int jumpHeight = 12;
        int dashCount;
        int shotCount;
        int maxFallDistance = 2500;
        int flyHeight = 5;

        float timer;
        float iFrameTimer = -1;
        float hitBoxOffset = 0.5f;

        double nextBlinkTime;
        double dashTimer;
        double dashCD;
        double shotTimer;
        double shotCD;

        double cooldownTimer = 0;

        double affectedTimer;

        bool downPlatform;
        bool blinking;
        public bool charmed;

        public Player(Texture2D tex, Vector2 pos, GameWindow window, List<PowerUp> powerUpList) : base(tex, pos)
        {
            charmed = false;
            isGrounded = true;
            //lives = 3;
            startPos = pos;
            velocity = new Vector2(0, 0);
            bY = window.ClientBounds.Height;
            bX = window.ClientBounds.Width;
            playerFacing = Facing.Right;
            this.powerUpList = powerUpList;
            //srRect = new Rectangle(tileSize.X * 0, tileSize.Y * 2, tileSize.X, tileSize.Y);
            //hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            dashCount = 1;
            dashTimer = 0;
            dashCD = 1.5;

            shotCount = 1;
            shotTimer = 0;
            shotCD = 2;

            affectedTimer = 0;

            isHit = false;
        }

        public override void HandleCollision(GameObject other)
        {
            velocity.Y = 0;
            isGrounded = true;

            if (other is EnemyBullet)
            {

                other.isActive = false;
                charmed = true;


                return;
            }

            if (other is EnemyType && other.isActive == true)
            {
                if (iFrameTimer <= 0 && GamePlayManager.health.hitBox.Width >= 0)
                {
                    GamePlayManager.health.hitBox.Width -= 20;
                }

                isHit = true;
                Console.WriteLine(GamePlayManager.health.hitBox.Width);
                return;
            }

            if (other is Item)
            {
                return;
            }

            if (other is PowerUp)
            {
                return;
            }

            if (other is Bullet)
            {
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

            if (GamePlayManager.health.hitBox.Width <= 0 || pos.Y > maxFallDistance) //Placeholder death "method".

            Console.WriteLine("vel from player" + velocity);

            if (GamePlayManager.health.hitBox.Width <= 0 || pos.Y > maxFallDistance) //Placeholder death "method".

            {
                pos = startPos;
                GamePlayManager.health.hitBox.Width = AssetManager.fullHealthbar.Width;
            }

            if (gt.TotalGameTime.TotalMilliseconds >= nextBlinkTime)
            {
                blinking = !blinking;
                nextBlinkTime = gt.TotalGameTime.TotalMilliseconds + 400;
            }

            foreach (Bullet b in bulletList)
            {
                b.Update(gt);
                b.Movement();
            }

            if (isGrounded)
            {
                downPlatform = true;
            }

            float i = 0.75f;
            velocity.Y += gravity * i; //falling faster and faster

            PlayerFacing();
            Charmed(gt);
            if (charmed == false)
            {
                Control();
            }
 
            PowerUp();
            ShootCooldown(gt);
            DashCooldown(gt);

            IFrame(gt);

            pos += velocity;
            hitBox.X = (int)(pos.X >= 0 ? pos.X + hitBoxOffset : pos.X - hitBoxOffset);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + hitBoxOffset : pos.Y - hitBoxOffset);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (iFrameTimer > 0)
            {
                if (blinking)
                    sb.Draw(tex, pos, null, Color.White, 0, Vector2.Zero, 1, effect, 0);
            }
            else if (charmed)
            {
                sb.Draw(AssetManager.playerCharmed, pos, null, Color.White, 0, Vector2.Zero, 1, effect, 0);
            }
            else
            {
                sb.Draw(tex, pos, null, Color.White, 0, Vector2.Zero, 1, effect, 0);
            }

            foreach (Bullet b in bulletList)
            {
                b.Draw(sb);
            }
        }

        void PowerUp()
        {
            foreach (PowerUp powerUp in powerUpList)
            {
                if (powerUp.poweredUp)
                {
                    if (powerUp.GetTexture == AssetManager.wing)
                        FlyingPowerUp();
                    else if (powerUp.GetTexture == AssetManager.boots)
                        JumpingPowerUp();
                }

                if (!powerUp.poweredUp)
                {
                    jumpHeight = 12;
                    if (powerUp.GetTexture == AssetManager.wing)
                        gravity = 1f;
                }
            }
        }

        public void PlayerFacing()
        {
            if (playerFacing == Facing.Right)
            {
                effect = SpriteEffects.None;
            }
            else if (playerFacing == Facing.Left)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
        }

        public void Control()
        {
            if (KeyMouseReader.keyState.IsKeyDown(Keys.D))
            {
                velocity.X = normalVel;

                playerFacing = Facing.Right;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(Keys.A))// && pos.X >= startPos.X
            {
                velocity.X = -normalVel;

                playerFacing = Facing.Left;
            }
            else
                velocity.X = 0;

            if (KeyMouseReader.mouseState.X >= (bX / 2))
            {
                playerFacing = Facing.Right;
            }

            else if (KeyMouseReader.mouseState.X <= (bX / 2))
            {
                playerFacing = Facing.Left;
            }

            //else
            //    playerFacing = Facing.Idle;

            if (KeyMouseReader.keyState.IsKeyDown(Keys.LeftShift))
            {
                if (dashCount > 0)
                {
                    if (KeyMouseReader.keyState.IsKeyDown(Keys.D))
                        velocity.X += dashLength;
                    if (KeyMouseReader.keyState.IsKeyDown(Keys.A))
                        velocity.X -= dashLength;

                    //save dash snapshot here
                    dashCount = 0;
                }
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Space) && isGrounded)
            {
                velocity.Y = -jumpHeight; //jump height               
                isGrounded = false;
                playerFacing = Facing.Jump;
                //srRect = new Rectangle(tileSize.X * 3, tileSize.Y * 0, tileSize.X, tileSize.Y);
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.S) && isGrounded)
            {
                velocity.Y = jumpHeight;
                isGrounded = false;
                downPlatform = false;
                playerFacing = Facing.Jump;
                //srRect = new Rectangle(tileSize.X * 3, tileSize.Y * 0, tileSize.X, tileSize.Y);
            }

            if (KeyMouseReader.LeftClick())
            {
                if (shotCount > 0)
                {
                    bullet = new Bullet(AssetManager.bomb, pos, playerFacing);
                    bulletList.Add(bullet);
                    bullet.isActive = true;

                    shotCount = 0;
                }
            }
        }

        public void IFrame(GameTime gameTime)
        {
            if (iFrameTimer <= 0 && isHit == true)
            {
                iFrameTimer = 3;
            }

            isHit = false;

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            iFrameTimer -= (int)timer;

            if (timer >= 1.0F)
            {
                timer = 0F;
            }
        }

        public void DashCooldown(GameTime gt)
        {
            if (dashCount <= 0)
                dashTimer += gt.ElapsedGameTime.TotalSeconds;

            double cooldown = 1.5;

            if (dashTimer >= cooldown && dashCount == 0)
            {
                dashCount = 1;
                dashTimer = 0;
            }

            Console.WriteLine(dashTimer);
        }

        public void Charmed(GameTime gt)
        {
            if (charmed == true)
                affectedTimer += gt.ElapsedGameTime.TotalSeconds;

            double charmDuration = 2;

            if (affectedTimer >= charmDuration && charmed == true)
            {
                charmed = false;
                if (charmed == false)
                    affectedTimer = 0;
            }
        }

        public void ShootCooldown(GameTime gt)
        {
            if (shotCount <= 0)
                shotTimer += gt.ElapsedGameTime.TotalSeconds;

            double cooldown = 1;

            if (shotTimer >= cooldown && shotCount == 0)
            {
                shotCount = 1;
                if (shotCount == 1)
                    shotTimer = 0;
            }
        }

        public void FlyingPowerUp()
        {
            if (KeyMouseReader.keyState.IsKeyDown(Keys.W))
            {
                velocity.Y = -flyHeight;
                isGrounded = false;

                if (!isGrounded)
                    gravity = 0.1f;
            }
            if (KeyMouseReader.keyState.IsKeyDown(Keys.S) && !isGrounded)
            {
                velocity.Y = flyHeight;
                isGrounded = false;
            }
            if (KeyMouseReader.keyState.IsKeyDown(Keys.D) && !isGrounded)
            {
                velocity.X = flyHeight;
                isGrounded = false;
            }
            if (KeyMouseReader.keyState.IsKeyDown(Keys.A) && !isGrounded)
            {
                velocity.X = -flyHeight;
                isGrounded = false;
            }
        }

        public void JumpingPowerUp()
        {
            jumpHeight = 20;
            //jumpHeight *= 2; not working like it should
        }

        public override Vector2 Position
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }

        public float Velocity
        {
            get
            {
                return velocity.X;
            }
            set
            {
                velocity.X = value;
            }
        }
    }
}

/* Reusabale code
 
            //public void Cooldown(GameTime gt, int count, double cooldown, double cooldownTimer)
        //{
        //    if (count <= 0)
        //        cooldownTimer += gt.ElapsedGameTime.TotalSeconds;

        //    if (cooldownTimer >= cooldown && count == 0)
        //    {
        //        count = 1;
        //        if (count == 1)
        //            cooldownTimer = 0;
        //    }

        //    Console.WriteLine(count);
        //}

    // public void Cooldown2(GameTime gt, int count, double cooldown)
    // When we dash: dashUsedSnapshot = gt.TotalGameTime.TotalSeconds;
    // 
 
 */
